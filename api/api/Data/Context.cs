using api.DTOs;
using api.Models.OrderAggregate;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=DESKTOP-DTGB06O; Database=SneakerMini; Trusted_connection=true; TrustServerCertificate=true");
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ShopCart> ShopCarts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Role>()
                .HasData(new Role { ID = 1, Name = "Admin" },
                        new Role { ID = 2, Name = "User" });

            modelBuilder.Entity<User>()
               .HasData(new User
               {
                   ID = 1,
                   UserName = "Hachi",
                   Email = "Hachi@gmail.com", 
                   Password = BCrypt.Net.BCrypt.HashPassword("Nn123@"),
                   RoleId = 1,
               });

            modelBuilder.Entity<Product>()
               .HasData(new Product
               {
                   ID = 1,
                   Name = "Converse",
                   Brand = "Converse",
                   Image = "https://cf.shopee.co.th/file/12c9bf58558418d82fd1967b44678128",
                   Description = "string",
                   Price = 2300,
                   ProductImages = new List<ProductImage> { },
                   Quantity = 5,
                   Size = "9",
                   Source = " Made In U.S.A."
               }); 
        }
         
    }
}
