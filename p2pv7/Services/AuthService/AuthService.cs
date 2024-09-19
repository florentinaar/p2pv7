using Azure;
using Azure.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using p2pv7.Data;
using p2pv7.DTOs;
using p2pv7.Migrations;
using p2pv7.Models;
using System.Data;
using System.Diagnostics.Metrics;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;

namespace p2pv7.Services
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public bool Register(UserDto request)
        {
            var userExists = UserExists(request.Email).Result;

            if (request == null || userExists)
            {
                return false;
            }

            CreatePasswordHash(request.Password, out byte[] PasswordHash, out byte[] PasswordSalt);

            var user = new User()
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = PasswordHash,
                PasswordSalt = PasswordSalt,
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return true;
        }

        public async Task<ServiceResponse<LoginResponseDto>>SignInAsync(string email, string password)
        {
            var user = await _context.Users
               .FirstOrDefaultAsync(x => x.Email.ToLower()
               .Equals(email.ToLower()));

            if (user == null)
            {
                return new ServiceResponse<LoginResponseDto>
                {
                    Message = "user not found",
                    Success = false
                };
            }

            var token = _context.Users.Where(x => x.Email == email)
                .Select(t => t.Token)
                .First() ?? "";

            return new ServiceResponse<LoginResponseDto>
            {
                Token = token,
                Data = new LoginResponseDto
                {
                    Username = user.Username
                }
            };
        }
           
        public bool AssignRole(Guid id, Guid roleId)
        {
            var user = _context.Users.Find(id);
            var role = _context.Roles.Find(roleId);

            if (user == null || role == null)
                return false;

            var roleName = role.RoleName;

            user.Role = role;
            user.RoleName = roleName;

            _context.SaveChanges();

            return true;
        }

        public bool VerifyUser(Guid id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
                return false;

            user.Token = CreateToken(user);
            user.VerifiedAt = DateTime.Now;

            _context.SaveChanges();

            return true;

        }
     

        #region PrivateUtils
        private async Task<bool> UserExists(string email)
        {

            return await _context.Users.AnyAsync(user => user.Email.ToLower()
             .Equals(email.ToLower()));
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
               new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.RoleName)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddMonths(12),
                    signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        #endregion
    }
}
