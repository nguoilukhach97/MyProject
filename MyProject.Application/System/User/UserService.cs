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
            if (await _userManager.FindByEmailAsync(request.Email) != null)
            {
                var failResponseEmail = new AuthenticateResponse()
                {
                    Successed = false,
                    Errors = new Error() { Code = "register_failed", Description = CommonMessage.EmailAlreadyExists }
                };
                return failResponseEmail;
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

        public async Task<ResponseBase> UpdateAsync(Guid id, UpdateUserRequest request )
        {
            var failResponse = new AuthenticateResponse()
            {
                Successed = false,
                Errors = new Error() { Code = "change_pass_failure", Description = CommonMessage.UpdateFailed }
            };
            if (await _userManager.Users.AnyAsync(x=>x.Email == request.Email && x.Id != id))
            {
                var failEmail = new ResponseBase()
                {
                    Successed = false,
                    Errors = new Error() { Code = "update_failed", Description = CommonMessage.EmailAlreadyExists}

                };
                return failEmail;
            }
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return failResponse;
            }

            user.FirstName = request.FirtsName;
            user.LastName = request.LastName;
            user.Dob = request.Dob;
            user.Email = request.Email;
            user.PhoneNumber = request.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                var success = new ResponseBase()
                {
                    Successed = true,
                    Errors = new Error() { Code = "update_success", Description = CommonMessage.UpdateSuccessed }

                };
                return success;
            }
            return failResponse;
        }

        public async Task<ResponseBase> UpdateRoles(Guid id, ListRoles role)
        {
            var response = new ResponseBase()
            {
                Successed=false,
                Errors = new Error() { Code ="update_role_failed",Description = CommonMessage.UpdateRoleFailed}
            };
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                response.Errors.Description = CommonMessage.CannotFind;
                return response;
            }
            var removeRoles = role.Roles.Where(x => x.IsSelected == false).Select(x => x.Name).ToList();
            await _userManager.RemoveFromRolesAsync(user,removeRoles);
            var addRoles = role.Roles.Where(x => x.IsSelected).Select(x => x.Name).ToList();
            foreach (var item in addRoles)
            {
                if (await _userManager.IsInRoleAsync(user,item) == false)
                {
                    await _userManager.AddToRoleAsync(user, item);
                }
                
            }
            var responseSucced = new ResponseBase()
            {
                Successed = true,
                Errors = new Error() { Code = "update_role_success", Description = CommonMessage.UpdateRoleSuccessed }
            };

            return responseSucced;

        }

        public async Task<ResponseBase> DeleteAsync(Guid id)
        {
            var response = new ResponseBase()
            {
                Successed = false,
                Errors = new Error() { Code = "delete_failed", Description = CommonMessage.DeleteFailed}
            };
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                response.Errors.Description = CommonMessage.CannotFind;
                return response;
            }
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                var responseSuccessed = new ResponseBase()
                {
                    Successed = true,
                    Errors = new Error() { Code = "delete_success", Description = CommonMessage.DeleteSuccessed }
                };
                return responseSuccessed;
            }
            return response;
        }

        public async Task<UserResponse<UserViewModel>> GetUserById(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new UserResponse<UserViewModel>()
                {
                    Successed = false,
                    Errors = new Error() { Code = "not_find_user", Description = CommonMessage.FindUserByIdFailed }
                };
            }
            var roles = await _userManager.GetRolesAsync(user);
            var result = new UserViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Dob = user.Dob,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName,
                Roles = roles
            };
            var successed = new UserResponse<UserViewModel>()
            {
                Object = result,
                Successed = true,
                Errors = new Error() { Code = "find_user_successed", Description = CommonMessage.FindUserByIdSuccessed }
            };
            
            return successed;
        }


        public async Task<Pagination<UserViewModel>> GetUserPaging(SearchingBase request)
        {
            var query = _userManager.Users;
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.UserName.Contains(request.Keyword) || x.PhoneNumber.Contains(request.Keyword)
                || x.Email.Contains(request.Keyword));
            }

            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)
                .Select(x => new UserViewModel()
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    PhoneNumber = x.PhoneNumber,
                    Dob = x.Dob,
                    Email = x.Email,
                    UserName = x.UserName
                }).ToListAsync();

            var pagedResult = new Pagination<UserViewModel>()
            {
                CurrentPage = request.PageIndex,
                PageSize = request.PageSize,
                TotalPages = totalRow % request.PageSize == 0 ? totalRow / request.PageSize : totalRow / request.PageSize + 1,
                TotalRows = totalRow,
                Data = data
            };

            return pagedResult;

           
        }
        #endregion
    }
}
