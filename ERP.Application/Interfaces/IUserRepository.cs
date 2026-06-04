using ERP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<AppUser?> GetByUsernameAsync(string username);

        Task CreateAsync(AppUser user);

        //Task<User?> GetByUsernameAsync(string username);

        //Task CreateAsync(User user);
    }
}
