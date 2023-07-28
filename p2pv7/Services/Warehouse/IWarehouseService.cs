using p2pv7.Models;

namespace p2pv7.Services
{
    public interface IWarehouseService
    {
        p2pv7.Models.Warehouse GetByID(int id);

    }
}
