using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.Extensions;
using Passenger.Infrastructure.IoC;
using Passenger.Api.Framework;
using Passenger.Infrastructure.Services;
using Passenger.Infrastructure.Settings;
using System.Text;
using Passenger.Infrastructure.Mongo;

namespace Passenger
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public ILifetimeScope AutofacContainer { get; private set; }


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.WriteIndented = true;
            });

            // Pobranie appsetingsow (appsetings.json)
            var jwtSettings = Configuration.GetSettings<JwtSettings>();
            var key = Encoding.ASCII.GetBytes(jwtSettings.Key);

            services.AddAuthorization(x => x.AddPolicy("admin", p => p.RequireRole("admin")));
            services.AddMemoryCache();


            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidIssuer = jwtSettings.Issuer
                };
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new ContainerModule(Configuration));
            
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime appLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseMiddleware(typeof(ExceptionHandlerMiddleware));
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            MongoConfigurator.Initializer();
            AutofacContainer = app.ApplicationServices.GetAutofacRoot();
            var generalSettings = app.ApplicationServices.GetService<GeneralSettings>();
            var repo = app.ApplicationServices.GetService<IUserRepository>();

            if(generalSettings.SeedData)
            {
                var dataInitializer = app.ApplicationServices.GetService<IDataInitializer>();
                dataInitializer.SeedAsync();
            }
        }
    }
}
