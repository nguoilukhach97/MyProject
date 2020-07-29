using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyProject.Application.ModelRequestService.ServiceRequest.User;
using MyProject.Application.System.User;

namespace MyProject.API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public UserController(IUserService userService,
            IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest requets)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requets);
            }
            var resultToken = await _userService.AuthenticateAsync(requets);
            if (!resultToken.Successed)
            {
                return BadRequest(requets);
            }
            return Ok(resultToken.AccessToken);
        }
        
        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest requets)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            //var result = await _userService.Register(requets);
            //if (!result)
            //{
            //    return BadRequest("Register khong duoc");
            //}
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> ChangePassword(Guid id, string currentPass, string newPass)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _userService.ChangePassword(id,currentPass,newPass);
            if (result.Successed)
            {
                return Ok(result.Errors);
            }
            return BadRequest(result.Errors);
        }

        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetUserPagingRequest request)
        {
            var data = await _userService.GetUserPaging(request);

            return Ok(data);

        }
        
    }
}
