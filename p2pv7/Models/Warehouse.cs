using System.ComponentModel.DataAnnotations;

namespace p2pv7.Models
{
    public class Warehouse
    {
        [Key]
        public int WarehouseID { get; set; }
        public List<Shelf>? Shelves { get; set; }
        public int UserID { get; set; }

    }
}
