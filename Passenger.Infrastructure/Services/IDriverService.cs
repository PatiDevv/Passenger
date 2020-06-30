using Passenger.Core.Domain;
using Passenger.Infrastructure.Commands.Drivers;
using Passenger.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services
{
    public interface IDriverService
    {
        Task CreateAsync(Guid userId);
        Task<DriverDto> GetAsync(Guid userId);
        Task<IEnumerable<DriverDto>> BrowseAsync();
        Task SetVehicleAsync(Guid userId, string brand, string name, int seats);
    }
}