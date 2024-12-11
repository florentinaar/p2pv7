using p2pv7.Models;

namespace p2pv7.Services
{
    public interface IPrintService
    {
        byte[] GenerateDoc(List<Order> order);
    }
}
