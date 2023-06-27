using p2pv7.DTOs;
using p2pv7.Models;

namespace p2pv7.Services
{
    public interface IAuthService
    {
        bool Register(UserDto request);
        string SignIn(string email, string password);
        bool AssignRole(Guid email, Guid roleId);
        bool VerifyUser(Guid id);
    }
}
