using Microsoft.EntityFrameworkCore;
using p2pv7.Models;

namespace p2pv7.Data
{
    public class DataContext :DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Business> Businesses { get; set; }
        public DbSet<Dimension> Dimensions { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
