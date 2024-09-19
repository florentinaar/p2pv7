using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using p2pv7.DTOs;
using p2pv7.Models;
using p2pv7.Services;

namespace p2pv7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("PostOrder")]
        public bool PostOrder(OrderDto order)
           => _orderService.PostOrder(order);

        [HttpGet("GetAllOrders")]
        public ActionResult<List<Order>> GetAllOrders()
            => Ok(_orderService.GetAllOrders());

        [HttpGet("GetOrdersByPrice")]
        public ActionResult<List<Order>> GetOrdersByPrice(double price)
            => Ok(_orderService.OrderFilterByPrice(price));

        [HttpGet("GetOrdersByAddress")]
        public ActionResult<List<Order>> GetOrdersByAdress(string address)
            => Ok(_orderService.OrderFiterByAddress(address));

        [HttpGet("GetOrdersByDate")]
        public ActionResult<List<Order>> GetOrdersByDate(DateTime date)
            => Ok(_orderService.OrderFiterByDate(date));

        [HttpGet("GetOrderById")]
        public Order GetOrder(Guid id)
            => _orderService.GetOrder(id);

        [HttpDelete]
        public ActionResult<List<Order>> DeleteOrder(Guid OrderId)
        {
            _orderService.DeleteOrder(OrderId);
            return Ok(_orderService.GetAllOrders());
        }

        [HttpPut("assignCourierToOrder")]
        public ActionResult<List<Order>> AssignCourierToOrder(Guid orderId, Guid courierId)
        {
            _orderService.assignCourierToOrder(orderId, courierId);
            return Ok();
        }

        [HttpGet("GetCourierOrder"), Authorize]
        public ActionResult<List<Order>> GetCourierOrders()
            => _orderService.GetCourierOrders();

        [HttpGet("CalculateSize")]
        public string CalculateSize(double length, double width, double height)
            => _orderService.CalculateSize(length, width, height);

        [HttpPost("SetOrderStatus")]
        public Order SetStatus(Guid? orderId, string status, Guid? courier)
        {
            if(orderId !=null && courier != null && !string.IsNullOrEmpty(status))
            {
                return _orderService.SetStatus(orderId.Value, status, courier.Value);
            }

            return new Order();
        }

        [HttpGet("mandants")]
        public MandantsListTree Mandants()
        {
            return new MandantsListTree()
            {
                Key = "t",
                Id = "t",
                Label = "t",
                Mandant = new Mandant()
                {
                    mandantAbrechNr = "1",
                    mandantShortText = "1",
                    MandantId = "t",
                    MandantNr = "1",
                    MandantType = MandantType.vlg,
                },
                Children = { }
            };
        }
    }

    public class MandantsListTree
    {
        public string Id { get; set; }
        public  Mandant Mandant { get; set; }
        public string Key { get; set; }
        public string Label { get; set; }
        public MandantsListTree Children { get; set; }
    }

    public class Mandant
    {
        public string MandantId { get; set; }
        public string MandantNr { get; set; }
        public MandantType MandantType { get; set; }
        public string mandantShortText { get; set; }
        public string mandantAbrechNr { get; set; }
    }

    public enum MandantType
    {
        vlg = 0
    }
}
