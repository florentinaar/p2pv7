using System.ComponentModel.DataAnnotations;

namespace p2pv7.Models
{
    public class Business
    {
        [Key]
        public int BusinessID { get; set; }
        public string BusinessName { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public string BusinessToken { get; set; }
    }
}
