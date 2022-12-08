namespace p2pv7.DTOs
{
    public class ProductDto
    {
        public Guid ProductId { get; set; }
        public string? Name { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Price { get; set; }
        public int ShelfId { get; set; } = 0;

    }
}
