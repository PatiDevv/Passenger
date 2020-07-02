using Passenger.Core.Repositories;
using Passenger.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Repositories
{
    public class InMemoryDriverRepository : IDriverRepository
    {
        private static ISet<Driver> _drivers = new HashSet<Driver>();

        public async Task<Driver> GetAsync(Guid userId)
            => await Task.FromResult (_drivers.SingleOrDefault(x => x.UserId == userId));

        public async Task <IEnumerable<Driver>> GetAllAsync()
            => await Task.FromResult (_drivers);

        public async Task RemoveAsync(Guid id)
        {
            var driver = await GetAsync(id);
            _drivers.Remove(driver);
            await Task.CompletedTask;
        }

        public async Task AddAsync(Driver driver)
        {
             _drivers.Add(driver);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Driver driver)
        {
            var oldDriver = _drivers.Single(d => d.UserId == driver.UserId);
            _drivers.Remove(oldDriver);
            _drivers.Add(driver);
            await Task.CompletedTask;
        }
    }
}
