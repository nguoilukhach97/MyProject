using MyProject.Application.ModelRequestService.ModelCommon;
using MyProject.Application.ModelRequestService.ServiceRequest.User;
using MyProject.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Service
{
    public interface IUserApi
    {
        Task<string> Authenticate(LoginRequest request);

        Task<Pagination<UserViewModel>> GetUserPaging(SearchingBase request);

        Task<bool> RegisterUser(RegisterRequest request);

    }
}
