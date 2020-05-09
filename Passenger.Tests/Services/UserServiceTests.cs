using AutoMapper;
using FluentAssertions;
using Moq;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.Services;
using Passenger.Tests.TestServices;
using System;
using System.Threading.Tasks;
using Xunit;


namespace Passenger.Tests.Services
{
    public class UserServiceTests
    {
        [Fact]
        public async Task register_async_should_invoke_add_async_on_repository()
        {

            var userRepositoryMock = new Mock<IUserRepository>();
            var mapperMock = new Mock<IMapper>();
            
            var userService = new UserService(userRepositoryMock.Object, mapperMock.Object);
            await userService.RegisterAsync("user1@email.com", "user", "secretttt", "kowalski");

            userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task registration_registered_user_should_throw_exception()
        {
            //Act

            var registered_user = new User ("arkadiuszchr@gmail.com", "arkadiusz", "secretttt", "secretttt", "arkadiusz chr");

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(x => x.GetAsync("arkadiuszchr@gmail.com")).Returns(Task.FromResult(registered_user));

            var mapperMock = new Mock<IMapper>();
            var userService = new UserService(userRepositoryMock.Object, mapperMock.Object);

            //Assert
            await Assert.ThrowsAsync<Exception>(() => userService.RegisterAsync("arkadiuszchr@gmail.com", "arkadiusz", "secretttt", "arkadiusz chr"));

        }

        [Fact]
        public void sum_of_10_and_24_should_return_34()
        {
            // bez moka
            IMathService mathService = new MathService();
            var sum = mathService.Sum(10, 24);
            sum.ShouldBeEquivalentTo(34);


            var mathServiceMock = new Mock<IMathService>();
            mathServiceMock.Setup(s => s.Sum(10, 24)).Returns(1);

            Assert.Equal(1, mathServiceMock.Object.Sum(10, 24));
            Assert.NotEqual(34, mathServiceMock.Object.Sum(10, 24));
        } 
    }
}
