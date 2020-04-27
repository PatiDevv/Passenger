using Passenger.Core.Repositories;
using Passenger.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Passenger.Infrastructure.Repositories
{
    public class InMemoryDriverRepository : IDriverRepository
    {
        private static ISet<Driver> _drivers = new HashSet<Driver>
        {
            new Driver(Guid.NewGuid(), Guid.NewGuid(), new Vehicle("Ford", "Focus", 5), new List<Route>{ new Route(),new Route()}, new List<DailyRoute>()),
            new Driver(Guid.NewGuid(), Guid.NewGuid(), new Vehicle("Ford", "Focus", 5), new List<Route>{ new Route(),new Route()}, new List<DailyRoute>()),
            new Driver(Guid.NewGuid(), Guid.NewGuid(), new Vehicle("Ford", "Focus", 5), new List<Route>{ new Route(),new Route()}, new List<DailyRoute>()),
            new Driver(Guid.NewGuid(), Guid.NewGuid(), new Vehicle("Ford", "Focus", 5), new List<Route>{ new Route(),new Route()}, new List<DailyRoute>())
        };
        public void Add(Driver driver)
        {
            _drivers.Add(driver);
        }

        public Driver Get(Guid userId)
            => _drivers.Single(x => x.UserId == userId);

        public IEnumerable<Driver> GetAll()
            => _drivers;

        public void Remove(Guid id)
        {
            var driver = Get(id);
            _drivers.Remove(driver);
        }

        public void Update(Driver driver)
        {
            var oldDriver = _drivers.Single(d => d.Id == driver.Id);
            _drivers.Remove(oldDriver);
            _drivers.Add(driver);
        }
    }
}
