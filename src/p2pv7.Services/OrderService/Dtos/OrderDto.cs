namespace p2pv7.DTOs
{
    public class OrderDto
    {
        public Guid OrderId { get; set; }
        public List<ProductDto> Products { get; set; }
        public DateTime Date { get; set; }
        public DateTime OrderedOn { get; set; }
        public double Price { get; set; }
        public string Address { get; set; }
        public string CompanyToken { get; set; }
        public int Phone { get; set; }
        public string Email { get; set; }

    }
}
