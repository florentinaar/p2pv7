using Azure;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using p2pv7.DTOs;
using p2pv7.Models;
using p2pv7.Services;
using p2pv7.Services.AuthService;
using p2pv7.Services.BusinessIntegration;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace p2pv7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public ActionResult<List<User>> Register(UserDto request)
        {
            _authService.Register(request);
            return Ok();
        }
        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(LoginDto request)
        {
            var response = await _authService.Login(request.Email, request.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
        [HttpPost("AssignRole"), Authorize(Roles ="admin")]
        public bool Assign(Guid id, string role)
        {
            _authService.AssignRole(id,role);
            return true;
        }
        [HttpPost("VerifyUser"), Authorize(Roles = "admin")]
        public bool VerifyUser(Guid id)
        {
            _authService.VerifyUser(id);
            return true;
        }

    }
}
