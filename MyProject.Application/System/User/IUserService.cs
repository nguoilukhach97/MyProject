using MyProject.Application.Auth.Dtos;
using MyProject.Application.ModelRequestService.ModelCommon;
using MyProject.Application.ModelRequestService.ServiceRequest.User;
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

        Task<UserViewModel> GetUserById(Guid id);
        Task<ApiResult<PagedViewResult<UserViewModel>>> GetUserPaging(GetUserPagingRequest request);
    }
}
