using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using p2pv7.Data;
using p2pv7.DTOs;
using p2pv7.Models;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace p2pv7.Services
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly UserManager<ApiUser> _userManager;
        private readonly IConfiguration _configuration;
        private ApiUser _user;

        public AuthService(DataContext context, IConfiguration configuration, UserManager<ApiUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<string> CreateToken()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var token = GenerateTokenOptions(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<bool> ValidateUser(LoginUserDto userDTO)
        {
            _user = await _userManager.FindByNameAsync(userDTO.Email);
            var validPassword = await _userManager.CheckPasswordAsync(_user, userDTO.Password);
            return (_user != null && validPassword);
        }

        //public bool AssignRole(Guid id, Guid roleId)
        //{
        //    var user = _context.Users.Find(id);
        //    var role = _context.Roles.Find(roleId);

        //    if (user == null || role == null)
        //        return false;

        //    var roleName = role.RoleName;

        //    user.Role = role;
        //    user.RoleName = roleName;

        //    _context.SaveChanges();

        //    return true;
        //}

        //public bool VerifyUser(Guid id)
        //{
        //    var user = _context.Users.Find(id);

        //    if (user == null)
        //        return false;

        //    user.Token = CreateToken(user);
        //    user.VerifiedAt = DateTime.Now;

        //    _context.SaveChanges();

        //    return true;

        //}
        #region Private Utils
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var expiration = DateTime.Now.AddMinutes(Convert.ToDouble(
                jwtSettings.GetSection("lifetime").Value));

            var token = new JwtSecurityToken(
                issuer: jwtSettings.GetSection("Issuer").Value,
                claims: claims,
                expires: expiration,
                signingCredentials: signingCredentials
                );

            return token;
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
             {
                 new Claim(ClaimTypes.Name, _user.UserName)
            };

            var roles = await _userManager.GetRolesAsync(_user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = _configuration.GetSection("JWT:Key").Value;
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

       

        #endregion
    }
}
