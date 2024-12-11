using p2pv7.DTOs;

namespace p2pv7.Services
{
    public interface IAuthService
    {
        //bool AssignRole(Guid email, Guid roleId);
        //bool VerifyUser(Guid id);
        Task<bool> ValidateUser(LoginUserDto userDTO);
        Task<string> CreateToken();
    }
}
