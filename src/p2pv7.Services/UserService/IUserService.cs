namespace p2pv7.Services
{
    public interface IUserService
    {
        string GetName();

        byte[] ActiveUsers();
    }
}
