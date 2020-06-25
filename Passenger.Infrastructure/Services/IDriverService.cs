using Passenger.Core.Domain;
using Passenger.Infrastructure.Commands.Drivers;
using Passenger.Infrastructure.DTO;
using System;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services
{
    public interface IDriverService
    {
        Task CreateAsync(Guid userId, DriverVehicle vehicle);
        Task<DriverDto> GetAsync(Guid userId);
    }
}