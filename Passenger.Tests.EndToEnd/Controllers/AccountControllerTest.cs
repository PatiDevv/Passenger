using FluentAssertions;
using Passenger.Infrastructure.Commands.Users;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Passenger.Tests.EndToEnd.Controllers
{
    
    public class AccountControllerTest : ControllerTestsBase
    { 
        [Fact]
        public async Task given_valid_current_and__new_password_it_should_be_changed()
        {
            //Act
            var command = new ChangeUserPassword
            {
               CurrentPassword = "secretttttt",
               NewPassword = "secretttt2"
            };

            var payload = GetPayload(command);
            var response = await Client.PutAsync("account/password", payload);

            response.StatusCode.ShouldBeEquivalentTo(HttpStatusCode.NoContent);
       
        }
    }
}
