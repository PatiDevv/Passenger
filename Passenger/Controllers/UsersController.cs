using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Passenger.Infrastructure.Services;
using Passenger.Infrastructure.DTO;

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
        public UserDto Get(string email)
                => _userService.Get(email);
        


    }
}
