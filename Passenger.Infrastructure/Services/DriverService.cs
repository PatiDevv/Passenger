using Passenger.Infrastructure.DTO;
using Passenger.Core.Repositories;
using System;
using System.Threading.Tasks;
using AutoMapper;
using Passenger.Core.Domain;
using Passenger.Infrastructure.Commands.Drivers;

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

        public async Task CreateAsync(Guid userId, DriverVehicle vehicle)
        {
            var user = await _userRepository.GetAsync(userId);
            if (user == null)
            {
                throw new Exception($"User with id: '{userId}' does not exists.");
            }

            var driver = await _driverRepository.GetAsync(userId);
            if (driver != null)
            {
                throw new Exception($"User with Id: '{userId}' is already assigned to the driver: {driver}");
            }

            var newdrivervehicle = Vehicle.Create(vehicle.Brand, vehicle.Name, vehicle.Seats);
            driver = new Driver(userId, newdrivervehicle, null, null);
            await _userRepository.AddAsync(user);
            await _driverRepository.AddAsync(driver);
        }
    }
}
