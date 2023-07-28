using Azure;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using p2pv7.DTOs;
using p2pv7.Migrations;
using p2pv7.Models;
using p2pv7.Services;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace p2pv7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        public AuthController(IAuthService authService, IUserService UserService)
        {
            _authService = authService;
            _userService = UserService;
        }

        [HttpPost("register")]
        public ActionResult<List<User>> Register(UserDto request)
        {
            _authService.Register(request);
            return Ok();
        }


        [HttpPost("login")]
        public string SignIn(string email, string password)
        {
            return _authService.SignIn(email, password);
        }

        //[HttpPost("AssignRole"), Authorize(Roles ="admin")]
        [HttpPost("AssignRole")]
        public bool Assign(Guid id, Guid roleId)
        {
            _authService.AssignRole(id, roleId);
            return true;
        }

        [HttpPost("VerifyUser")]
        public bool VerifyUser(Guid id)
        {
            _authService.VerifyUser(id);
            return true;
        }

        [HttpGet("getme"), Authorize]
        public ActionResult<string> getme()
        {
            return Ok(_userService.getName());
        }

    }
}
