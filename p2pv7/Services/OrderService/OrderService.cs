using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using p2pv7.Data;
using p2pv7.DTOs;
using p2pv7.Models;
using p2pv7.Services.UserService;

namespace p2pv7.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly DataContext _context;

        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        public OrderService(DataContext context, IMapper mapper, IUserService userService)
        {
            _context = context;
            _userService = userService;
            _mapper = mapper;

        }

        public Order GetOrder(Guid id)
        {
            var ordersFromDb = _context.Orders
                .Where(n => n.OrderId == id)
                .Include(w => w.Products)
                .FirstOrDefault();
            var orders = _context.Orders.FindAsync(id);
            var orderToReturn = _mapper.Map<Order>(ordersFromDb);
            return orderToReturn;
        }

        public ActionResult<List<Order>> GetAllOrders()
        {
            var orders = _context.Orders.Include(x => x.Products).ToList();
            return orders;
        }

        public bool PostOrder(OrderDto order)
        {
            if (order == null)
            {
                return false;
            }

            var bussinesExists = _context.Businesses
                .Where(x => x.BusinessToken == order.CompanyToken)
                .FirstOrDefault();

            if (bussinesExists == null)
            {
                return false;
            }

            var _order = new Order();
            _mapper.Map(order, _order);
            _context.Orders.Add(_order);
            _context.SaveChangesAsync();

            return true;
        }

        public bool DeleteOrder(Guid OrderId)
        {
            var order = _context.Orders.Find(OrderId);
            if (order == null)
            {
                return false;
            }
            else if (!OrderExists(order))
            {
                return false;
            }
            else
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
                return true;
            }
        }

        public bool OrderExists(Order request)
        {
            bool alreadyExist = _context.Orders.Any(x => x.OrderId == request.OrderId);
            return alreadyExist;
        }


        //public void setStatus(Guid orderId, string status, Guid courier)

        //{
        //    var ordersFromDb = _context.Orders
        //        .Where(n => n.OrderId == orderId)
        //        .Include(w => w.Products)
        //        .FirstOrDefault();

        //    if (ordersFromDb != null)
        //    {
        //        ordersFromDb.Status = status;
        //        if (status == "reject")
        //        {
        //            _context.Orders.Remove(ordersFromDb);
        //            _context.SaveChanges();
        //        }
        //        else if (status == "accept")
        //        {

        //            foreach (Product product in ordersFromDb.Products)
        //            {

        //                product.ShelfId = GetAvailableShelf();
        //                _context.SaveChanges();

        //            }
        //        }
        //        else if (status == "transfer")
        //        {
        //            assignCourierToOrder(orderId, courier);
        //            foreach (Product product in ordersFromDb.Products)
        //            {

        //                RemoveProductFromShelf(product.ProductId);
        //                _context.SaveChanges();

        //            }
        //        }
        //        _context.SaveChanges();
        //    };
        //}


        //public void RemoveProductFromShelf(Guid product)
        //{
        //    var _product = _context.Products
        //        .Find(product);

        //    var shelf = _context.Shelves
        //        .Find(_product.ShelfId);

        //    _product.ShelfId = 0;
        //    if (shelf != null)
        //    {
        //        shelf.AvailableSlots++;
        //    }
        //    _context.SaveChanges();

        //}
        //public int GetAvailableShelf()
        //{
        //    var freeShelf = _context.Shelves.Where(s => s.AvailableSlots > 0).FirstOrDefault();
        //    if (freeShelf != null)
        //    {
        //        freeShelf.AvailableSlots--;
        //        return freeShelf.ShelfId;
        //    }
        //    return 0;

        //}


        public void assignCourierToOrder(Guid orderId, Guid courierId)
        {
            var courier = _context.Users.Find(courierId);
            var order = _context.Orders.Find(orderId);
            if (order != null && courier != null)
            {
                order.CourierId = courierId;
                _context.SaveChanges();
            }
        }

        public List<Order> GetCourierOrders()
        {
            var currentUser = _userService.getName();
            var user = _context.Users
                .Where(x => x.Email == currentUser)
                .Select(x => x.UserId).First();
            var orders = _context.Orders
                .Where(x => x.CourierId == user)
                .Include(x => x.Products).ToList();
            return orders;
        }

        public string CalculateSize(double length, double width, double height)
        {

            var SPWidth = from d in _context.Dimensions
                          where d.name == "SmallPackage"
                          select d.width;
            var SmallPackageWidth = SPWidth.FirstOrDefault();

            var SPHeight = from d in _context.Dimensions
                           where d.name == "SmallPackage"
                           select d.height;
            var SmallPackageHeight = SPHeight.FirstOrDefault();

            var SPLength = from d in _context.Dimensions
                           where d.name == "SmallPackage"
                           select d.length;
            var SmallPackageLength = SPLength.FirstOrDefault();

            var MPHeight = from d in _context.Dimensions
                           where d.name == "MediumPackage"
                           select d.height;
            var MediumPackageHeight = MPHeight.FirstOrDefault();


            var MPWidth = from d in _context.Dimensions
                          where d.name == "MediumPackage"
                          select d.width;
            var MediumPackageWidth = MPWidth.FirstOrDefault();

            var MPLength = from d in _context.Dimensions
                           where d.name == "MediumPackage"
                           select d.length;
            var MediumPackageLength = MPLength.FirstOrDefault();

            var LPLength = from d in _context.Dimensions
                           where d.name == "LargePackage"
                           select d.length;
            var LargePackageLength = LPLength.FirstOrDefault();

            var LPwidth = from d in _context.Dimensions
                          where d.name == "LargePackage"
                          select d.width;
            var LargePackageWidth = LPwidth.FirstOrDefault();

            var LPHeight = from d in _context.Dimensions
                           where d.name == "LargePackage"
                           select d.height;
            var LargePackageHeight = LPHeight.FirstOrDefault();


            if (height < SmallPackageHeight && width < SmallPackageWidth && length < SmallPackageLength)
            {
                return ("this is a small package");
            }

            else if (height > SmallPackageHeight && height < MediumPackageHeight && width > SmallPackageWidth && width < MediumPackageWidth && length > SmallPackageLength && length < MediumPackageLength)
            {
                return ("this is a medium package");
            }
            else if (length > MediumPackageLength && length < LargePackageLength && width > MediumPackageWidth && width < LargePackageWidth && height > MediumPackageHeight && height < LargePackageHeight)
            {
                return ("this is a large package");
            }
            return ("we do not ship this kind of package, please contact our staff for further details");
        }
    }
}
