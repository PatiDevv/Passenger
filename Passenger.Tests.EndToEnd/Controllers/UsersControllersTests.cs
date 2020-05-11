using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Passenger.Infrastructure.Commands.Users;
using Passenger.Infrastructure.DTO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Passenger.Tests.EndToEnd.Controllers
{
    public class UsersControllersTests : ControllerTestsBase
    {

        [Fact]
        public async Task given_valid_email_user_should_exist()
        {
            //Act
            var email = "user1@mail.com";
            var user = await GetUserAsync(email);

            //Assert
            user.Email.ShouldBeEquivalentTo(email);
        }

        [Fact]
        public async Task given_invalid_email_user_should_not_exist()
        {
            //Act
            var email = "user1000@mail.com";
            var response = await Client.GetAsync($"users/{email}");
            
            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task given_unique_email_user_should_be_created()
        {
            //Act
            var commend = new CreateUser
            {
                Email = "blabla@email.com",
                Password = "secretttt",
                UserName = "test",
                FullName = "kowalski"
            };

            var payload = GetPayload(commend);
            var response = await Client.PostAsync("users", payload);

            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.Created);
            var actualLocation = response.Headers.Location.ToString();
            var expectedLocation = string.Format($"users/{commend.Email}");

            actualLocation.ShouldBeEquivalentTo(expectedLocation);

            var user = await GetUserAsync(commend.Email);
            user.Email.ShouldBeEquivalentTo(commend.Email);
        }

        private async Task<UserDto> GetUserAsync(string email)
        {
            var response = await Client.GetAsync($"users/{email}");
            var responseString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<UserDto>(responseString);
        }
    }
}
