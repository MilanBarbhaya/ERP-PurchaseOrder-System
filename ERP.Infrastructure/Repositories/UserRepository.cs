using ERP.Application.Interfaces;
using ERP.Domain.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<AppUser> _users;

        public UserRepository(IMongoDatabase db)
        {
            _users = db.GetCollection<AppUser>("Users");
        }

        public async Task<AppUser?> GetByUsernameAsync(string username)
        {
            return await _users
                .Find(x => x.Username == username)
                .FirstOrDefaultAsync();
        }

        public async Task CreateAsync(AppUser user)
        {
            await _users.InsertOneAsync(user);
        }
    }
}
