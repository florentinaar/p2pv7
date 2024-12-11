using Microsoft.AspNetCore.Identity;

namespace p2pv7.Models
{
    public class ApiUser : IdentityUser
    {
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
    }
}
