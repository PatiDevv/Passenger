using Passenger.Core.Domain;
using System;
using System.Collections.Generic;

namespace Passenger.Core.Repositories
{
    public interface IUserRepository
    {
        User Get(Guid Id);
        User Get(string email);
        IEnumerable<User> GetAll();
        void Add(User user);
        void Update(User user);
        void Remove(Guid id);
    }
}
