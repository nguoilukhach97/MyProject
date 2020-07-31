using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using MyProject.Application.ModelRequestService.ServiceRequest.User;
using MyProject.Common;
using MyProject.Service;

namespace MyProject.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserApi _userApi;
        private readonly IConfiguration _configuration;
        public UserController(IUserApi userApi,IConfiguration configuration)
        {
            _userApi = userApi;
            _configuration = configuration;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string keyword, int pageIndex=1, int pageSize=2)
        {
            //var sessions = HttpContext.Session.GetString("Token");
            var request = new SearchingBase()
            {
               
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _userApi.GetUserPaging(request);
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
               
                return View();
            }    
                

            var token = await _userApi.Authenticate(request);
            if (string.IsNullOrEmpty(token))
            {
                ViewBag.SuccessMsg = "Tài khoản hoặc mật khẩu sai !";
                return View(request);
            }
            var userPrincipal = this.ValidateToken(token);
            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                IsPersistent = true
            };
            //HttpContext.Session.SetString("Token",token);
            SetCookie(token);
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                userPrincipal,
                authProperties);
            

            return RedirectToAction("Index","Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //HttpContext.Session.Remove("Token");
            Response.Cookies.Delete("token");
            return RedirectToAction("Login","User");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _userApi.Delete(id);
            if (result.Successed)
            {
                TempData["result"] = "Xóa người dùng thành công";
                return RedirectToAction("Index","User");
            }

            ModelState.AddModelError("", result.Errors.Description);
            return View(ModelState);
        }

        [HttpGet]
        public IActionResult Register()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return View();
            var result = await _userApi.RegisterUser(request);
            if (result.Successed)
            {
                TempData["result"] = "Thêm mới người dùng thành công";
                return RedirectToAction("Login","User");
            }
            ViewBag.SuccessMsg = result.Errors.Description;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(Guid id, UpdateUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }
            var result = await _userApi.UpdateUser(id, request);
            if (result.Successed)
            {
                TempData["result"] = "Thêm mới người dùng thành công";
                return RedirectToAction("Index", "User");
            }
            TempData["result"] = result.Errors.Description;
            return RedirectToAction("Index", "User");

        }

        private ClaimsPrincipal ValidateToken(string JwtToken)
        {
            IdentityModelEventSource.ShowPII = true;
            SecurityToken validatedToken;
            TokenValidationParameters validationParameters = new TokenValidationParameters();
            validationParameters.ValidateLifetime = true;

            validationParameters.ValidAudience = _configuration["Tokens:Issuer"];
            validationParameters.ValidIssuer = _configuration["Tokens:Issuer"];
            validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));

            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(JwtToken,validationParameters,out validatedToken);

            return principal;

        }

        public void SetCookie(string value)
        {
            string key = "token";
            int expireTime = DateTime.Now.AddHours(1).Second;
            CookieOptions option = new CookieOptions();

            //if (expireTime.HasValue)
            //    option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
            //else
            //    option.Expires = DateTime.Now.AddMilliseconds(10);

            Response.Cookies.Append(key, value, option);
        }
    }
}
