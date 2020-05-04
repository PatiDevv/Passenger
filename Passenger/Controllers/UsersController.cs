using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Passenger.Infrastructure.Services;
using Passenger.Infrastructure.DTO;
using Passenger.Infrastructure.Commands.Users;

namespace Passenger.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userServices)
        {
            _userService = userServices;
        }

        [HttpGet("{email}")]
        public async Task <UserDto> GetAsync(string email)
                => await _userService.GetAsync(email);

        [HttpPost]
        public async Task Post([FromBody]CreateUser request)
        => await _userService.RegisterAsync(request.Email, request.UserName, request.Password, request.FullName);
        

    }
}
