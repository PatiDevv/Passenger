using Microsoft.Extensions.Configuration;


namespace Passenger.Infrastructure.Extensions
{
    public static class SettingsExtensions
    {
        public static T GetSettings<T>(this IConfiguration configuration) where T : new()
        {
            var section = typeof(T).Name.Replace("Settings", string.Empty); // general
            var configurationValue = new T(); // new GeneralSettings()
            configuration.GetSection(section).Bind(configurationValue); // appsettings > general binduje do GeneralSettings

            return configurationValue;
        }
    }
}
