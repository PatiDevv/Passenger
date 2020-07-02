using System;
using System.Collections.Generic;
using System.Text;

namespace Passenger.Infrastructure.DTO
{
    public class DriverDetailsDto : DriverDto
    {
        public IEnumerable<RouteDto> Routes { get; set; }
    }
}
