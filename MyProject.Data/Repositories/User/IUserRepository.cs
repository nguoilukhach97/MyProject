using MyProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Data.Repositories.User
{
    public interface IUserRepository
    {
        Task<AppUser> GetAsync(Guid id);
        Task<AppUser> GetAllAsync();
        Task<AppUser> AddAsync(AppUser user);
        Task UpdateAsync(AppUser user);
        Task DeleteAsync(AppUser user);
    }
}
