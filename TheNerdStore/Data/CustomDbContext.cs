using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheNerdStore.Models;

namespace TheNerdStore.Data
{
    public class CustomDbContext : DbContext
    {
        public CustomDbContext(DbContextOptions<CustomDbContext> option) : base(option)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {  // <Product> ---> ProductName --- Price --- ImageURL --- Stock
            modelBuilder.Entity<Product>().HasData(
                new { ID = 1, ProductName = "", Price = .99m, ImageURL = "", Stock = 100 },
                new { ID = 2, ProductName = "", Price = 1.99m, ImageURL = "", Stock = 142 },
                new { ID = 3, ProductName = "", Price = 1.99m, ImageURL = "", Stock = 643 },
                new { ID = 4, ProductName = "", Price = 1.59m, ImageURL = "", Stock = 236 },
                new { ID = 5, ProductName = "", Price = 1.15m, ImageURL = "", Stock = 100 },
                new { ID = 6, ProductName = "", Price = 1.00m, ImageURL = "", Stock = 100 },
                new { ID = 7, ProductName = "", Price = 1.00m, ImageURL = "", Stock = 100 },
                new { ID = 8, ProductName = "", Price = 1.09m, ImageURL = "", Stock = 100 },
                new { ID = 9, ProductName = "", Price = .99m, ImageURL = "", Stock = 100 },
                new { ID = 10, ProductName = "", Price = .99m, ImageURL = "", Stock = 100 }
                );
        }
    }
}
