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

namespace p2pv7.Services.AuthService
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
            //var userexists = UserExists(request.Email);
            if (request == null)
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

        public async Task<ServiceResponse<string>> Login(string email, string password)
        {
            var response = new ServiceResponse<string>();
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email.ToLower()
                .Equals(email.ToLower()));

            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found.";
            }
            else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Wrong password.";
            }
            else
            {
                response.IDs = user.UserId;
                response.Data = user.Username;
                string token = CreateToken(user);
                response.Message = token;
            }

            return response;
        }

        public bool AssignRole(Guid id, Guid roleId)
        {
            var user = _context.Users
               .FirstOrDefault();
            var role = _context.Roles.Find(roleId);
            var roleName = role.RoleName;
            if (user == null)
            {
                return false;
            }
            if (role == null)
            {
                return false;
            }

            var currentUser = _context.Users.Find(id);
            if (currentUser != null)
            {
                currentUser.Role = role;
                currentUser.RoleName = roleName;
            }

            _context.SaveChanges();
            return true;
        }
        public bool VerifyUser(Guid id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return false;
            }
            user.Token = CreateToken(user);
            user.VerifiedAt = DateTime.Now;
            _context.SaveChanges();
            return true;

        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        public async Task<bool> UserExists(string email)
        {

            return await _context.Users.AnyAsync(user => user.Email.ToLower()
             .Equals(email.ToLower()));

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


    }
}
