using Microsoft.AspNetCore.Mvc;
using Passenger.Infrastructure.Commands;
using System;
using System.Threading.Tasks;

namespace Passenger.Api.Controllers
{
    [Route("[controller]")]
    public abstract class ApiControllerBase : Controller
    {
        protected readonly ICommandDispatcher _commandDispatcher;

        protected Guid UserId => User?.Identity?.IsAuthenticated == true ?
            Guid.Parse(User.Identity.Name) :
            Guid.Empty;

        protected ApiControllerBase(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        protected async Task DispatchAsync<T>(T command) where T : ICommand
        {
            if(command is AuthenticatedCommandBase authenticatedCommand)
            {
                authenticatedCommand.UserId = UserId;
            }
            await _commandDispatcher.DispatchAsync(command);
        }
    }
}
