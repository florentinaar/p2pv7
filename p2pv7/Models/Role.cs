using System.ComponentModel.DataAnnotations;

namespace p2pv7.Models
{
    public class Role
    {
        [Key]
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public List<User>? Users { get; set; }
    }
}
