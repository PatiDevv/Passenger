using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services
{
    public class DataInitializer : IDataInitializer
    {
        private readonly IUserService _userService;
        private readonly ILogger<DataInitializer> _logger;
        private readonly IDriverService _driverService;
        private readonly IDriverRouteService _driverRouteService;

        public DataInitializer(IUserService userService, IDriverService driverService, IDriverRouteService driverRouteService, ILogger<DataInitializer> logger)
        {
            _userService = userService;
            _driverService = driverService;
            _driverRouteService = driverRouteService;
            _logger = logger;
        }

        public async Task SeedAsync()
        {
            var users = await _userService.BrowseAsync();
            if (users.Any())
            {
                return;
            }

            _logger.LogTrace("Initializing data...");
            var tasks = new List<Task>();

            for(var i=1 ; i<=10; i++)
            {
                var userId = Guid.NewGuid();
                var username = $"user{i}";

                await _userService.RegisterAsync(userId, $"{username}@test.com", username, "secret", "kowalski", "user");
                _logger.LogTrace($"Created a new user: '{username}'.");
               
                await _driverService.CreateAsync(userId);
                await _driverService.SetVehicleAsync(userId, "BMW", "i8");
                _logger.LogTrace($"Created a new driver for: '{username}'.");

               await _driverRouteService.AddAsync(userId, "Default route", 1, 1, 2, 2);
               await _driverRouteService.AddAsync(userId, "Job route", 3, 3, 4, 4);
                _logger.LogTrace($"Adding route for: '{username}'");
            }

            for (var i = 1; i <= 3; i++)
            {
                var userId = Guid.NewGuid();
                var username = $"admin{i}";
                _logger.LogTrace($"Created a new admin: '{username}'.");

                await _userService.RegisterAsync(userId, $"{username}@test.com", username, "secret", "kowalski", "admin");
            }
            _logger.LogTrace("Data was initialized.");
        }
    }
}
