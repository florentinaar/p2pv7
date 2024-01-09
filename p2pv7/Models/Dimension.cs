using System.ComponentModel.DataAnnotations;

namespace p2pv7.Models
{
    public class Dimension
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Length { get; set; }
        public bool InUse { get; set; }
    }
}
