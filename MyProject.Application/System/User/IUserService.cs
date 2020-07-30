using MyProject.Application.Auth.Dtos;
using MyProject.Application.ModelRequestService.ModelCommon;
using MyProject.Application.ModelRequestService.ServiceRequest.User;
using MyProject.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Application.System.User
{
    public interface IUserService
    {
        Task<AuthenticateResponse> AuthenticateAsync(LoginRequest request);
        
        Task<ResponseBase> Register(RegisterRequest request);

        Task<ResponseBase> ChangePassword(Guid id, string currentPass, string newPass);

        Task<ResponseBase> UpdateAsync(Guid id, UpdateUserRequest request);
        Task<ResponseBase> DeleteAsync(Guid id);
        Task<UserResponse<UserViewModel>> GetUserById(Guid id);
        Task<Pagination<UserViewModel>> GetUserPaging(SearchingBase request);
    }
}
