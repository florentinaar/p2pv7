using System.Security.Claims;

namespace p2pv7.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(IHttpContextAccessor httpContextAccessor) {
            _httpContextAccessor = httpContextAccessor;
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
