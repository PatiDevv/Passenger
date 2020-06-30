using Passenger.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services
{
    public interface IUserService : IService
    {
        Task <UserDto> GetAsync(string email);
        Task RegisterAsync(Guid userId, string email, string username, string password, string fullname, string role);
        Task LoginAsync(string email, string password);
        Task<IEnumerable<UserDto>> BrowseAsync();
    }
}
