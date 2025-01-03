﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using p2pv7.Data;
using p2pv7.DTOs;
using p2pv7.Models;

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
            var ordersFromDb = _context.Orders.Where(n => n.OrderId == id)
                .Include(w => w.Products)
                .FirstOrDefault();

            return ordersFromDb ?? new Order();
        }

        public List<Order> GetAllOrders()
        {
            var orders = _context.Orders.Include(x => x.Products).ToList();

            return orders;
        }

        public List<Order> GetAllOrdersToList()
           => _context.Orders.Include(x => x.Products).ToList();

        public List<Order> OrderFilterByPrice(double price)
           => _context.Orders.Include(x => x.Products).Where(m => m.Price == price).ToList();

        public List<Order> OrderFiterByAddress(string address)
           => _context.Orders.Include(x => x.Products).Where(m => m.Address == address).ToList();
        public List<Order> OrderFiterByDate(DateTime date)
           => _context.Orders.Include(x => x.Products).Where(m => m.Date == date).ToList();

        public bool PostOrder(OrderDto order)
        {
            if (order == null)
                return false;

            var bussinesExists = _context.Businesses
                .Where(x => x.BusinessToken == order.CompanyToken)
                .FirstOrDefault();

            if (bussinesExists == null)
                return false;

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

        public Order SetStatus(Guid orderId, string status, Guid courier)
        {
            var order = _context.Orders
                .Where(n => n.OrderId == orderId)
                .Include(w => w.Products)
                .FirstOrDefault();

            if (order != null)
            {
                order.Status = status;
                if (status == "reject")
                {
                    _context.Orders.Remove(order);
                    _context.SaveChanges();
                }
                //else if (status == "accept")
                //{

                //    foreach (Product product in ordersFromDb.Products)
                //    {

                //        product.ShelfId = GetAvailableShelf();
                //        _context.SaveChanges();

                //    }
                //}
                //else if (status == "transfer")
                //{
                //    assignCourierToOrder(orderId, courier);
                //    foreach (Product product in ordersFromDb.Products)
                //    {

                //        RemoveProductFromShelf(product.ProductId);
                //        _context.SaveChanges();

                //    }
                //}
                _context.SaveChanges();
                return order;
            }
            return new Order();
        }


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
            var currentUser = _userService.GetName();

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

            var SmallPackageWidth = (from d in _context.Dimensions
                                     where d.Name == "SmallPackage"
                                     select d.Width).FirstOrDefault();

            var SmallPackageHeight = (from d in _context.Dimensions
                                      where d.Name == "SmallPackage"
                                      select d.Height).FirstOrDefault();

            var SmallPackageLength = (from d in _context.Dimensions
                                      where d.Name == "SmallPackage"
                                      select d.Length).FirstOrDefault();

            var MediumPackageHeight = (from d in _context.Dimensions
                                       where d.Name == "MediumPackage"
                                       select d.Height).FirstOrDefault();

            var MediumPackageWidth = (from d in _context.Dimensions
                                      where d.Name == "MediumPackage"
                                      select d.Width).FirstOrDefault();

            var MediumPackageLength = (from d in _context.Dimensions
                                       where d.Name == "MediumPackage"
                                       select d.Length).FirstOrDefault();

            var LargePackageLength = (from d in _context.Dimensions
                                      where d.Name == "LargePackage"
                                      select d.Length).FirstOrDefault();

            var LargePackageWidth = (from d in _context.Dimensions
                                     where d.Name == "LargePackage"
                                     select d.Width).FirstOrDefault();

            var LargePackageHeight = (from d in _context.Dimensions where d.Name == "LargePackage" select d.Height).FirstOrDefault();


            if (height < SmallPackageHeight && width < SmallPackageWidth && length < SmallPackageLength)
                return ("this is a small package");
            else if (height > SmallPackageHeight && height < MediumPackageHeight && width > SmallPackageWidth && width < MediumPackageWidth && length > SmallPackageLength && length < MediumPackageLength)
                return ("this is a medium package");
            else if (length > MediumPackageLength && length < LargePackageLength && width > MediumPackageWidth && width < LargePackageWidth && height > MediumPackageHeight && height < LargePackageHeight)
                return ("this is a large package");

            return ("we do not ship this kind of package, please contact our staff for further details");
        }

        #region PrivateUtils
        private bool OrderExists(Order request)
            => _context.Orders.Any(x => x.OrderId == request.OrderId);

        #endregion
    }
}
