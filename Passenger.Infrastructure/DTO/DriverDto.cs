using Passenger.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Passenger.Infrastructure.DTO
{
    public class DriverDto
    {
        public Guid UserId { get; protected set; }
        public string Name { get; protected set; }
        public DateTime UpdateAt { get; protected set; }
    }
}
