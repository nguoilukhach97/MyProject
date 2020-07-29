 using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyProject.Application.ModelRequestService.ModelCommon;
using MyProject.Application.ModelRequestService.ServiceRequest.User;
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

        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            RoleManager<AppRole> roleManager,
            IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config; 
        }
        public async Task<ApiResult<string>> Authenticate(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null) return new ApiResult<string>(false, "Người dùng không tồn tại");

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
            if (!result.Succeeded)
            {
                return new ApiResult<string>(false, "Sai mật khẩu");
            }
            var roles = _userManager.GetRolesAsync(user);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.LastName),
                new Claim(ClaimTypes.Role,string.Join(";",roles)),
                new Claim(ClaimTypes.Name,request.UserName)
            };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);

            var successToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new ApiResult<string>
            {
                IsSuccessed = true,
                Result = successToken

            };
        }

        

        public async Task<ApiResult<bool>> Register(RegisterRequest request)
        {
            var checkUser = await _userManager.FindByNameAsync(request.UserName);
            if (checkUser != null)
            {
                return new ApiResult<bool>(false,"Tài khoản đã tồn tại ");
            }
            if (await _userManager.FindByEmailAsync(request.Email) != null)
            {

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

            var result = await _userManager.CreateAsync(user,request.Password);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }


        public async Task<ApiResult<PagedViewResult<UserViewModel>>> GetUserPaging(GetUserPagingRequest request)
        {
            var query = _userManager.Users;
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.UserName.Contains(request.Keyword) || x.PhoneNumber.Contains(request.Keyword));
            }

            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.pageIndex - 1) * request.pageSize).Take(request.pageSize)
                .Select(x => new UserViewModel()
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    PhoneNumber =x.PhoneNumber,
                    Email = x.Email,
                    UserName = x.UserName
                }).ToListAsync();

            var pagedResult = new PagedViewResult<UserViewModel>()
            {
                TotalRecord = totalRow,
                Items = data
            };

            return pagedResult;
        }

    }
}
