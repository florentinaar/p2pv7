using AutoMapper;
using p2pv7.DTOs;
using p2pv7.Models;

namespace p2pv7.Mappings
{
    public class OrderMapper :Profile
    {
        public OrderMapper()
        {
            CreateMap<Order, Product>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}
