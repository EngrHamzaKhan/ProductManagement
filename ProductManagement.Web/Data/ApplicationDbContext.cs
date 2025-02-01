using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Web.Models;

namespace ProductManagement.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Laptop", Price = 1000, Quantity = 10 },
                new Product { Id = 2, Name = "Smartphone", Price = 700, Quantity = 15 },
                new Product { Id = 3, Name = "Tablet", Price = 300, Quantity = 20 },
                new Product { Id = 4, Name = "Smartwatch", Price = 150, Quantity = 50 },
                new Product { Id = 5, Name = "Headphones", Price = 100, Quantity = 30 },
                new Product { Id = 6, Name = "Monitor", Price = 200, Quantity = 25 },
                new Product { Id = 7, Name = "Keyboard", Price = 50, Quantity = 40 },
                new Product { Id = 8, Name = "Mouse", Price = 30, Quantity = 60 },
                new Product { Id = 9, Name = "Charger", Price = 20, Quantity = 100 },
                new Product { Id = 10, Name = "Laptop Stand", Price = 40, Quantity = 70 },
                new Product { Id = 11, Name = "Bluetooth Speaker", Price = 120, Quantity = 35 },
                new Product { Id = 12, Name = "Wireless Mouse", Price = 45, Quantity = 40 },
                new Product { Id = 13, Name = "External Hard Drive", Price = 80, Quantity = 25 },
                new Product { Id = 14, Name = "USB Flash Drive", Price = 15, Quantity = 90 },
                new Product { Id = 15, Name = "Graphics Card", Price = 500, Quantity = 10 },
                new Product { Id = 16, Name = "CPU", Price = 300, Quantity = 15 },
                new Product { Id = 17, Name = "Webcam", Price = 80, Quantity = 50 },
                new Product { Id = 18, Name = "Router", Price = 60, Quantity = 20 },
                new Product { Id = 19, Name = "AirPods", Price = 150, Quantity = 30 },
                new Product { Id = 20, Name = "Portable Speaker", Price = 70, Quantity = 45 }
            );
        }

    }
}
