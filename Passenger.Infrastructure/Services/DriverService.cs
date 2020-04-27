using Passenger.Infrastructure.DTO;
using Passenger.Core.Repositories;
using System;

namespace Passenger.Infrastructure.Services
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;

        public DriverService(IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }

        public DriverDto Get(Guid userId)
        {
            var driver = _driverRepository.Get(userId);

            return new DriverDto
            {
                DailyRoutes = driver.DailyRoutes,
                Id = driver.Id,
                Routes = driver.Routes,
                Vehicle = driver.Vehicle
            };
        }
    }
}
