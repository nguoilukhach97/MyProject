using MyProject.Application.ModelRequestService.ServiceRequest.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Application.System.User
{
    public class UserService : IUserService
    {
        public Task<bool> Authenticate(LoginRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Register()
        {
            throw new NotImplementedException();
        }
    }
}
