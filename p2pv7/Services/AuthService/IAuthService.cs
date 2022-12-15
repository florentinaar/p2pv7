using p2pv7.DTOs;
using p2pv7.Models;

namespace p2pv7.Services.AuthService
{
    public interface IAuthService
    {
        bool Register(UserDto request);
        Task<ServiceResponse<string>> Login(string email, string password);
        //Task<ServiceResponse<string>> AssignRole(string email, string role);
        bool AssignRole(Guid email, Guid roleId);
        bool VerifyUser(Guid id);
    }
}
