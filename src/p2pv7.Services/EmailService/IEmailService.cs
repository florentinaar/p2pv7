using p2pv7.DTOs;

namespace p2pv7.Services
{
    public interface IEmailService
    {
        void SendEmail(EmailDto request);
    }
}
