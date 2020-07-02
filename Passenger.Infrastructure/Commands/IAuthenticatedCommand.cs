using System;
using System.Collections.Generic;
using System.Text;

namespace Passenger.Infrastructure.Commands
{
    public interface IAuthenticatedCommand : ICommand
    {
        Guid UserId { get; set; }
    }
}
