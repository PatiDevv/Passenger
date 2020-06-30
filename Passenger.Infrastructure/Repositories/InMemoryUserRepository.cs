using System;
using System.Collections.Generic;
using System.Text;
using Passenger.Core.Repositories;
using Passenger.Core.Domain;
using System.Linq;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Repositories
{
    public class InMemoryUserRepository : IUserRepository
    {
        private static ISet<User> _users = new HashSet<User>
        {
            new User("user1@mail.com", "user1", "se345345cret", "salt", "fullname", "admin"),
            new User("user2@mail.com", "user1", "se345345cret", "salt", "fullname", "admin"),
            new User("user3@mail.com", "user1", "sec345345ret", "salt", "fullname", "user"),
        };

        public async Task<User> GetAsync(Guid Id)
             => await Task.FromResult(_users.Single(x => x.Id == Id));

        public async Task<User> GetAsync(string email)
             => await Task.FromResult (_users.SingleOrDefault(x => x.Email == email.ToLowerInvariant()));

        public async Task<IEnumerable<User>> GetAllAsync()
             => await Task.FromResult(_users);

        public async Task AddAsync(User user)
        {
            _users.Add(user);
            await Task.CompletedTask;
        }
        public async Task RemoveAsync(Guid id)
        {
            var user = await GetAsync(id);
            _users.Remove(user);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(User user)
        {
            await Task.CompletedTask;
        }
    }
}
