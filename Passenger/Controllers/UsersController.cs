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
        public async Task<IActionResult> Get(string email)
        { 
        var user = await _userService.GetAsync(email);
            if(user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task <IActionResult> Post([FromBody]CreateUser request)
        {  
            await _userService.RegisterAsync(request.Email, request.UserName, request.Password, request.FullName);

            return Created($"users/{request.Email}", new object());

        }
        

    }
}
