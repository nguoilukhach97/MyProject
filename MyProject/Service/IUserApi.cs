using MyProject.Application.ModelRequestService.ServiceRequest.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Service
{
    public interface IUserApi
    {
        Task<string> Authenticate(LoginRequest request);

    }
}
