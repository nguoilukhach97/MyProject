using System;
using System.Collections.Generic;
using System.Linq;
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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest requets)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resultToken = await _userService.Authenticate(requets);
            if (string.IsNullOrEmpty(resultToken))
            {
                return BadRequest("Username or Password is incorrect");
            }
            return Ok( new { token= resultToken});
        }
        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest requets)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userService.Register(requets);
            if (!result)
            {
                return BadRequest("Register khong duoc");
            }
            return Ok();
        }
    }
}
