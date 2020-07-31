using MyProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Data.Repositories.User
{
    public class UserRepository : IUserRepository
    {
        private readonly IUserRepository _user;
        public UserRepository(IUserRepository user)
        {
            _user = user;
        }
        public async Task<AppUser> GetAsync(Guid id)
        {
            var user = await _user.GetAsync(id);
            return user;
        }
        public async Task<AppUser> GetAllAsync()
        {
            var users = await _user.GetAllAsync();
            return users;
        }
        public async Task<AppUser> AddAsync(AppUser app)
        {
            await _user.AddAsync(app);
            return app;
        }
        public async Task UpdateAsync(AppUser user)
        {
            await _user.UpdateAsync(user);
        }
        public async Task DeleteAsync(AppUser user)
        {
            await _user.DeleteAsync(user);
        }

    }
}
