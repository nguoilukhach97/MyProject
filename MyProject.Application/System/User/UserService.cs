 using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyProject.Application.Auth;
using MyProject.Application.Auth.Dtos;
using MyProject.Application.ModelRequestService.ModelCommon;
using MyProject.Application.ModelRequestService.ServiceRequest.User;
using MyProject.Common;
using MyProject.Data.Entities;
using MyProject.Utilities.Exceptions;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Application.System.User
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _config;
        private readonly IJwtToken _jwtToken;

        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            RoleManager<AppRole> roleManager,
            IConfiguration config,IJwtToken jwtToken)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jwtToken = jwtToken;
            _config = config; 
        }
        
        


        public async Task<ApiResult<PagedViewResult<UserViewModel>>> GetUserPaging(GetUserPagingRequest request)
        {
            //var query = _userManager.Users;
            //if (!string.IsNullOrEmpty(request.Keyword))
            //{
            //    query = query.Where(x => x.UserName.Contains(request.Keyword) || x.PhoneNumber.Contains(request.Keyword));
            //}

            //int totalRow = await query.CountAsync();

            //var data = await query.Skip((request.pageIndex - 1) * request.pageSize).Take(request.pageSize)
            //    .Select(x => new UserViewModel()
            //    {
            //        Id = x.Id,
            //        FirstName = x.FirstName,
            //        LastName = x.LastName,
            //        PhoneNumber =x.PhoneNumber,
            //        Email = x.Email,
            //        UserName = x.UserName
            //    }).ToListAsync();

            //var pagedResult = new PagedViewResult<UserViewModel>()
            //{
            //    TotalRecord = totalRow,
            //    Items = data
            //};

            //return pagedResult;

            throw new Exception();
        }


        #region

        public async Task<AuthenticateResponse> AuthenticateAsync(LoginRequest request)
        {
            var failResponse = new AuthenticateResponse()
            {
                Successed = false,
                Errors = new Error() {Code = "login_failure",Description= CommonMessage.LoginFailed }
            };
            if (request == null || string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password))
            {
                return failResponse;
            }
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user== null)
            {
                return failResponse;
            }    
            var checkPass = await _signInManager.PasswordSignInAsync(request.UserName,request.Password,request.RememberMe,false);

            if (!checkPass.Succeeded)
            {
                return failResponse;
            }                
           
            var roles = await _userManager.GetRolesAsync(user);
            string token = _jwtToken.GenerateToken(user.Id,roles,user.UserName);

            var resultSuccess = new AuthenticateResponse()
            {
                AccessToken = token,
                Successed = true
            };
            return resultSuccess;
        }

        public async Task<ResponseBase> Register(RegisterRequest request)
        {
            var failResponse = new AuthenticateResponse()
            {
                Successed = false,
                Errors = new Error() { Code = "register_failure", Description = CommonMessage.RegisterFailed }
            };

            var checkUser = await _userManager.FindByNameAsync(request.UserName);

            if (checkUser != null)
            {
                return failResponse;
            }
            var user = new AppUser()
            {
                Dob = request.Dob,
                Email = request.Email,
                FirstName = request.FirtsName,
                LastName = request.LastName,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                var successResponse = new AuthenticateResponse()
                {
                    Successed = true,
                    Errors = new Error() { Code = "register_success", Description = CommonMessage.RegisterSuccessed }
                };
                return successResponse;
            }
            return failResponse;


        }

        public async Task<ResponseBase> ChangePassword(Guid id,string currentPass, string newPass)
        {
            var failResponse = new AuthenticateResponse()
            {
                Successed = false,
                Errors = new Error() { Code = "change_pass_failure", Description = CommonMessage.ChangePasswordFail }
            };

            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return failResponse;
            }
            else
            {
                var result = await _userManager.ChangePasswordAsync(user,currentPass, newPass);
                if (result.Succeeded)
                {
                    var successResponse = new AuthenticateResponse()
                    {
                        Successed = true,
                        Errors = new Error() { Code = "change_pass_success", Description = CommonMessage.ChangePasswordSuccessed }
                    };
                    return successResponse;
                }
                return failResponse;
            }
        }

        public async Task<UserViewModel> GetUserById(Guid id)
        {
            var user = new UserViewModel();
            var ruser = await _userManager.FindByIdAsync(id.ToString());
        }
        #endregion
    }
}
