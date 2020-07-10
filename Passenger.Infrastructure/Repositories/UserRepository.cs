using Passenger.Core.Repositories;
using Passenger.Core.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Passenger.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

namespace Passenger.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository, ISqlRepository
    {
        private readonly PassengerContext _context;

        public UserRepository(PassengerContext context)
        {
            _context = context;
        }

        public async Task<User> GetAsync(Guid Id)
        => await _context.Users.SingleOrDefaultAsync(x => x.Id == Id);

        public async Task<User> GetAsync(string email)
        => await _context.Users.SingleOrDefaultAsync(x => x.Email == email);

        public async Task<IEnumerable<User>> GetAllAsync()
        => await _context.Users.ToListAsync();

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Guid id)
        {
            var user = await GetAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {   
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

    }
}

// UserRepository MongoDB

//namespace Passenger.Infrastructure.Repositories
//{
//    public class UserRepository : IUserRepository, IMongoRepository
//    {
//        private readonly IMongoDatabase _database;

//        public UserRepository(IMongoDatabase database)
//        {
//            _database = database;
//        }

//        public async Task<User> GetAsync(Guid Id)
//        => await Users.AsQueryable().FirstOrDefaultAsync(x => x.Id == Id);

//        public async Task<User> GetAsync(string email)
//        {
//            var dupa = Users.AsQueryable().ToList();
//            return await Users.AsQueryable().FirstOrDefaultAsync(x => x.Email == email);
//        }

//        public async Task<IEnumerable<User>> GetAllAsync()
//        => await Users.AsQueryable().ToListAsync();

//        public async Task AddAsync(User user)
//        => await Users.InsertOneAsync(user);

//        public async Task RemoveAsync(Guid id)
//        => await Users.DeleteOneAsync(x => x.Id == id);

//        public async Task UpdateAsync(User user)
//        => await Users.ReplaceOneAsync(x => x.Id == user.Id, user);

//        private IMongoCollection<User> Users => _database.GetCollection<User>("Users");
//    }
//}
