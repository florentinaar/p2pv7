using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using p2pv7.Data;
using p2pv7.DTOs;
using p2pv7.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace p2pv7.Services
{
    public class BusinessIntegrationService : IBusinessIntegrationService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        public BusinessIntegrationService(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public List<Business> GetAllBusinesses()
            => _context.Businesses.ToList();

        public Business GetBussinesByToken(string token)
        {
            Business business = new Business();

            business = _context.Businesses.Where(x => x.BusinessToken == token).FirstOrDefault();

            return business ?? new Business();
        }

        public bool SaveBusiness(BusinessDto request)
        {
            if (request == null)
            {
                return false;
            }

            var alreadyExist = _context.Businesses
                .Where(x => x.BusinessName == request.BusinessName & x.Email == request.Email)
                .FirstOrDefault();

            if (alreadyExist != null)
            {
                return false;
            }

            var business = new Business();
            business.BusinessName = request.BusinessName;
            business.Email = request.Email;
            business.PhoneNumber = request.PhoneNumber;
            business.BusinessToken = CreateToken(business);

            _context.Businesses.Add(business);
            _context.SaveChangesAsync();

            return true;
        }

        #region PrivateUtils
        private string CreateToken(Business business)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, business.BusinessName),
                new Claim(ClaimTypes.Email, business.Email)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
        #endregion
    }
}
