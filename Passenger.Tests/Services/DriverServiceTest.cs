using AutoMapper;
using Moq;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.Commands.Drivers;
using Passenger.Infrastructure.Exceptions;
using Passenger.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Passenger.Tests.Services
{
    public class DriverServiceTest
    {
        [Fact] 
        public async Task while_creating_driver_service_should_throw_exeption_if_user_null()
        {
            // Arrange
            var driverRepository = new Mock<IDriverRepository>();
            var userRepository = new Mock<IUserRepository>();
            var mapperMock = new Mock<IMapper>();
            var vehicleProvider = new Mock<IVehicleProvider>();

            var _driverService = new DriverService(driverRepository.Object, mapperMock.Object, userRepository.Object, vehicleProvider.Object);

            var userId = Guid.NewGuid();

            // Act 
            var creatingDriverTask = _driverService.CreateAsync(userId);

            // Assert
            await Assert.ThrowsAsync<Exception>(() => creatingDriverTask);
        }

        [Fact]
        public async Task while_creating_driver_service_should_throw_exeption_if_user_already_have_driver()
        {

            // Arrange
            var driverRepository = new Mock<IDriverRepository>();
            var userRepository = new Mock<IUserRepository>();
            var mapperMock = new Mock<IMapper>();
            var vehicleProvider = new Mock<IVehicleProvider>();

            var registered_user = new User(Guid.NewGuid(), "arkadiuszchrr@gmail.com", "arkadiusz", "secretttt", "secretttt", "arkadiusz chr", "admin");
            userRepository.Setup(x => x.GetAsync(registered_user.Id)).Returns(Task.FromResult(registered_user));

            var driver = new Driver(registered_user.Id, null, new List<Route>(), new List<DailyRoute>());
            driverRepository.Setup(x => x.GetAsync(registered_user.Id)).Returns(Task.FromResult(driver));


            var _driverService = new DriverService(driverRepository.Object, mapperMock.Object, userRepository.Object, vehicleProvider.Object);

            // Act
            var creatingDriverTask = _driverService.CreateAsync(registered_user.Id);

            // Assert
            await Assert.ThrowsAsync<ServiceException>(() => creatingDriverTask);
        }

        [Fact]
        public async Task creating_driver_should_pass()
        {
            // Arrange
            var driverRepository = new Mock<IDriverRepository>();
            var userRepository = new Mock<IUserRepository>();
            var mapperMock = new Mock<IMapper>();
            var vehicleProvider = new Mock<IVehicleProvider>();

            var registered_user = new User(Guid.NewGuid(), "arkadiuszchr@gmail.com", "arkadiusz", "secretttt", "secretttt", "arkadiusz chr", "admin");
            userRepository.Setup(x => x.GetAsync(registered_user.Id)).Returns(Task.FromResult(registered_user));

            var _driverService = new DriverService(driverRepository.Object, mapperMock.Object, userRepository.Object, vehicleProvider.Object);

            // Act 
            var creatingDriverTask = _driverService.CreateAsync(registered_user.Id);
            await creatingDriverTask;

            // Assert
            Assert.True(creatingDriverTask.IsCompleted);
        }
    }
}
