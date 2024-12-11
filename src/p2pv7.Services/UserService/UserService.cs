using Microsoft.AspNetCore.Http;
using p2pv7.Data;
using System.Security.Claims;
using System.Text;

namespace p2pv7.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataContext _context;
        public UserService(IHttpContextAccessor httpContextAccessor, DataContext context) {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public byte[] ActiveUsers()
        {
            var users = _context.Users.Where(x => x.VerifiedAt != null);

            var encoding = Encoding.GetEncoding("iso-8859-1");
            StringWriter sw = new StringWriter();

            sw.WriteLine("Name; Email; FiscalNumber; AuthorizeExchangeData; AuthorizeOwnData; DocumentSendDate");
            foreach (var g in users)
            {
                sw.WriteLine(string.Format("{0};{1};{2};{3};{4}", g.UserId, g.Email, g.Username, g.CompanyName, g.VerifiedAt));
            }
            var stream = encoding.GetBytes(sw.ToString());

            return stream;
        }

        public string GetName()
        {
            var user = string.Empty;

            if (_httpContextAccessor !=null)
            {
                user = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            }

            return user;
        }
    }
}
