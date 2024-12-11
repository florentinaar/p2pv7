using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using p2pv7.DTOs;
using p2pv7.Models;
using p2pv7.Services;

namespace p2pv7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly UserManager<ApiUser> _userManager;
        public AuthController(IAuthService authService, IUserService UserService,
            IMapper mapper,
            UserManager<ApiUser> userManager
            )
        {
            _authService = authService;
            _userService = UserService;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] UserDto userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = _mapper.Map<ApiUser>(userDTO);
                user.UserName = userDTO.Email;
                var result = await _userManager.CreateAsync(user, userDTO.Password);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return BadRequest(ModelState);
                }
                await _userManager.AddToRolesAsync(user, userDTO.Roles);
                return Accepted();
            }
            catch (Exception ex)
            {
                return Problem($"Something Went Wrong in the {nameof(Register)}", statusCode: 500);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (!await _authService.ValidateUser(userDTO))
                {
                    return Unauthorized();
                }

                return Accepted(new { Token = await _authService.CreateToken() });
            }
            catch (Exception ex)
            {
                return Problem($"Something Went Wrong in the {nameof(Login)}", statusCode: 500);
            }
        }

       
        [HttpGet("GetMe"), Authorize]
        public ActionResult<string> GetMe() => Ok(_userService.GetName());

        [HttpGet("ActiveUsersCSV")]
        public ActionResult CreateCsv()
        {
            var stream = _userService.ActiveUsers();

            return File(stream, "text/csv", "ActiveUsersList.csv");
        }

        //[HttpPost("AssignRole"), Authorize(Roles ="admin")]
        // [HttpPost("AssignRole")]
        //public bool Assign(Guid id, Guid roleId)
        //   => _authService.AssignRole(id, roleId);

        //[HttpPost("VerifyUser")]
        //public bool VerifyUser(Guid id)
        //   =>  _authService.VerifyUser(id);


    }
}
