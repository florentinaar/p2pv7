﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using p2pv7.Configurations.Entities;
using p2pv7.Models;

namespace p2pv7.Data
{
    public class DataContext : IdentityDbContext<ApiUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {}
        public DbSet<Order> Orders { get; set; }
        public DbSet<Business> Businesses { get; set; }
        public DbSet<Dimension> Dimensions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RoleConfiguration());
        }
    }
}
