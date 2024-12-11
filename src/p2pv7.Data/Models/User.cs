using System.ComponentModel.DataAnnotations;

namespace p2pv7.Models
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public string Email { get; set; } 
        public string? CompanyName { get; set; }
        public Role? Role { get; set; }
        public Guid? RoleId { get; set; }
        public string RoleName { get; set; } = "NoRole";
        public string? Token { get; set; }
        public DateTime? VerifiedAt { get; set; }
        //public string RefreshToken { get; set; } = string.Empty;
        //public DateTime TokenCreated { get; set; }
        //public DateTime TokenExpires { get; set; }
    }
}
