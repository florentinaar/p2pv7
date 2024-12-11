using p2pv7.DTOs;
using p2pv7.Models;

namespace p2pv7.Services
{
    public interface IOrderService
    {
        Order GetOrder(Guid id);
        List<Order> GetAllOrders();
        bool PostOrder(OrderDto order);
        bool DeleteOrder(Guid OrderId);
        //void setStatus(Guid orderId, string status, Guid courier);
        void assignCourierToOrder(Guid orderId, Guid courierId);
        string CalculateSize(double length, double width, double height);
        List<Order> GetCourierOrders();
        Order SetStatus(Guid orderId, string status, Guid courier);
        List<Order> GetAllOrdersToList();
        List<Order> OrderFilterByPrice(double price);
        List<Order> OrderFiterByAddress(string address);
        List<Order> OrderFiterByDate(DateTime date);
    }
}
