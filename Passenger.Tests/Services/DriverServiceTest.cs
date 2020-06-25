using AutoMapper;
using Moq;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.Commands.Drivers;
using Passenger.Infrastructure.Services;
using System;
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

            var _driverService = new DriverService(driverRepository.Object, mapperMock.Object, userRepository.Object);

            var userId = Guid.NewGuid();
            var driverVehicle = new DriverVehicle();

            // Act 
            var creatingDriverTask = _driverService.CreateAsync(userId, driverVehicle);

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

            var registered_user = new User("arkadiuszchr@gmail.com", "arkadiusz", "secretttt", "secretttt", "arkadiusz chr");
            userRepository.Setup(x => x.GetAsync(registered_user.Id)).Returns(Task.FromResult(registered_user));

            var driver = new Driver(registered_user.Id, null, null, null);
            driverRepository.Setup(x => x.GetAsync(registered_user.Id)).Returns(Task.FromResult(driver));


            var _driverService = new DriverService(driverRepository.Object, mapperMock.Object, userRepository.Object);

            // Act
            var creatingDriverTask = _driverService.CreateAsync(registered_user.Id, null);

            // Assert
            await Assert.ThrowsAsync<Exception>(() => creatingDriverTask);
        }

        [Fact]
        public async Task creating_driver_should_pass()
        {
            // Arrange
            var driverRepository = new Mock<IDriverRepository>();
            var userRepository = new Mock<IUserRepository>();
            var mapperMock = new Mock<IMapper>();

            var registered_user = new User("arkadiuszchr@gmail.com", "arkadiusz", "secretttt", "secretttt", "arkadiusz chr");
            userRepository.Setup(x => x.GetAsync(registered_user.Id)).Returns(Task.FromResult(registered_user));

            var _driverService = new DriverService(driverRepository.Object, mapperMock.Object, userRepository.Object);


            // Act 
            var creatingDriverTask = _driverService.CreateAsync(registered_user.Id, new DriverVehicle { Brand = "asasd", Name = "asdasd", Seats = 4 });
            await creatingDriverTask;

            // Assert
            Assert.True(creatingDriverTask.IsCompleted);
        }
    }
}
