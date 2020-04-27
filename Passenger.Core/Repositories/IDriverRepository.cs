using Passenger.Core.Domain;
using System;
using System.Collections.Generic;

namespace Passenger.Core.Repositories
{
    public interface IDriverRepository
    {
        Driver Get(Guid userId);
        IEnumerable<Driver> GetAll();
        void Add(Driver driver);
        void Update(Driver driver);
        void Remove(Guid id);
    }
}
