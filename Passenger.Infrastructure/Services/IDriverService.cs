using Passenger.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Passenger.Infrastructure.Services
{
    public interface IDriverService
    {
        DriverDto Get(Guid userId);
    }
}
