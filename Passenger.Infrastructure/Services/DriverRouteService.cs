using AutoMapper;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services
{
    class DriverRouteService : IDriverRouteService
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IMapper _mapper;

        public DriverRouteService(IDriverRepository driverRepository, IMapper mapper)
        {
            _driverRepository = driverRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(Guid userId, string name, double startLatitude, double startLongitude, double endLatitude, double endLongitude)
        {
            var driver = await _driverRepository.GetAsync(userId);
            if(driver == null)
            {
                throw new Exception($"Driver with user id: '{userId}' was not found");
            }
            var start = Node.Create("Start adress", startLongitude, startLatitude);
            var end = Node.Create("End adress", endLongitude, endLatitude);
            driver.AddRoute(name, start, end);
            await _driverRepository.UpdateAsync(driver);
        }

        public async Task DeleteAsync(Guid userId, string name)
        {
            var driver = await _driverRepository.GetAsync(userId);
            if (driver == null)
            {
                throw new Exception($"Driver with user id: '{userId}' was not found");
            }
            driver.DeleteRoute(name);
            await _driverRepository.UpdateAsync(driver);
        }
    }
}
