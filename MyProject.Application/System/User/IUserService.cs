using MyProject.Application.ModelRequestService.ServiceRequest.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Application.System.User
{
    public interface IUserService
    {
        Task<bool> Authenticate(LoginRequest request);
        Task<bool> Register();
    }
}
