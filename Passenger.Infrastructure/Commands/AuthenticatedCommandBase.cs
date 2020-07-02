using System;
using System.Collections.Generic;
using System.Text;

namespace Passenger.Infrastructure.Commands
{
    public class AuthenticatedCommandBase : IAuthenticatedCommand
    {
        public Guid UserId { get; set; }
    }
}
