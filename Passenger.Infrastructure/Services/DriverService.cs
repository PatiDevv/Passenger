using Passenger.Infrastructure.DTO;
using Passenger.Core.Repositories;
using System;
using System.Threading.Tasks;
using AutoMapper;
using Passenger.Core.Domain;
using Passenger.Infrastructure.Commands.Drivers;
using System.Collections.Generic;

namespace Passenger.Infrastructure.Services
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public DriverService(IDriverRepository driverRepository, IMapper mapper, IUserRepository userRepository)
        {
            _driverRepository = driverRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<DriverDto> GetAsync(Guid userId)
        {
            var driver = await _driverRepository.GetAsync(userId);

            return _mapper.Map<Driver, DriverDto>(driver);
        }

        public async Task<IEnumerable<DriverDto>> BrowseAsync()
        {
            var drivers = await _driverRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<Driver>, IEnumerable<DriverDto>>(drivers);
        }

        public async Task CreateAsync(Guid userId)
        {
            var user = await _userRepository.GetAsync(userId);
            if (user == null)
            {
                throw new Exception($"User with id: '{userId}' was not found.");
            }

            var driver = await _driverRepository.GetAsync(userId);
            if (driver != null)
            {
                throw new Exception($"Driver with Id: '{userId}' already exists.");
            }

            driver = new Driver(user);
           
            await _driverRepository.AddAsync(driver);
        }

        public async Task SetVehicleAsync(Guid userId, string brand, string name, int seats)
        {
            var driver = await _driverRepository.GetAsync(userId);
            if (driver == null)
            {
                throw new Exception($"Driver with user Id: '{userId}' was not found.");
            }
            driver.SetVehicle(brand, name, seats);
            await _driverRepository.UpdateAsync(driver);
        }
    }
}
