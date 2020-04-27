using System;
using System.Collections.Generic;
using System.Text;
using Passenger.Core.Repositories;
using Passenger.Core.Domain;
using System.Linq;

namespace Passenger.Infrastructure.Repositories
{
    public class InMemoryUserRepository : IUserRepository
    {
        private static ISet<User> _users = new HashSet<User>
        {
            new User("USER1@GAMIL.COM", "user1", "secret", "salt", "fullname"),
            new User("USER2@GAMIL.COM", "user1", "secret", "salt", "fullname"),
            new User("USER3@GAMIL.COM", "user1", "secret", "salt", "fullname"),
        };

        public void Add(User user)
        {
            _users.Add(user);
        }

        public User Get(Guid Id)
            => _users.Single(x => x.Id == Id);

        public User Get(string email)
         => _users.Single(x => x.Email == email.ToLowerInvariant());

        public IEnumerable<User> GetAll()
            => _users;
        public void Remove(Guid id)
        {
            var user = Get(id);
            _users.Remove(user);
        }

        public void Update(User user)
        {
        }
    }
}
