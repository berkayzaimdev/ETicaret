using ETicaret.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Runtime.Intrinsics.X86;
using System.Xml.Linq;

namespace ETicaret.Data
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<Cart> Carts { get; set; }  
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Shipper> Shippers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<RecentlyViewedProduct> RecentlyViewedProducts { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder) 
        {
            base.OnModelCreating(builder);
            builder.Entity<AppRole>().HasData(new AppRole
            {
                Id = 1,
                Name = "Admin",
                NormalizedName = "ADMIN"
            });

            builder.Entity<AppRole>().HasData(new AppRole
            {
                Id = 2,
                Name = "User",
                NormalizedName = "USER"
            });

            AppUser user0 = new()
            {
                Id = 1,
                Name = "admin",
                Surname = "admin",
                Email = "admin@gmail.com",
                Gender = Enum.Parse<Gender>("Male"),
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                ImageUrl = "/img/defaultpp.png",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            AppUser user1 = new()
            {
                Id = 2,
                Name = "John",
                Surname = "Doe",
                Email = "johndoe@gmail.com",
                Gender = Enum.Parse<Gender>("Male"),
                UserName = "johndoe",
                NormalizedUserName = "JOHNDOE",
                ImageUrl = "/img/defaultpp.png",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            AppUser user2 = new()
            {
                Id = 3,
                Name = "Jane",
                Surname = "Doe",
                Email = "janedoe@gmail.com",
                Gender = Enum.Parse<Gender>("Female"),
                UserName = "janedoe",
                NormalizedUserName = "JANEDOE",
                ImageUrl = "/img/defaultpp.png",
                
                SecurityStamp = Guid.NewGuid().ToString()
            };

            PasswordHasher<AppUser> ph = new PasswordHasher<AppUser>();
            user0.PasswordHash = ph.HashPassword(user0, "Passw.1");
            user1.PasswordHash = ph.HashPassword(user1, "Passw.1");
            user2.PasswordHash = ph.HashPassword(user2, "Passw.1");

            builder.Entity<AppUser>().HasData(user0);
            builder.Entity<AppUser>().HasData(user1);
            builder.Entity<AppUser>().HasData(user2);

            builder.Entity<IdentityUserRole<int>>().HasData(new IdentityUserRole<int>
            {
                RoleId = 1,
                UserId = 1
            });

            builder.Entity<IdentityUserRole<int>>().HasData(new IdentityUserRole<int>
            {
                RoleId = 2,
                UserId = 2
            });

            builder.Entity<IdentityUserRole<int>>().HasData(new IdentityUserRole<int>
            {
                RoleId = 2,
                UserId = 3
            });

            builder.Entity<Cart>()
            .HasMany(c => c.CartItems)      
            .WithOne(ci => ci.Cart)        
            .HasForeignKey(ci => ci.CartId);

            builder.Entity<Cart>().HasData(new Cart
            {
                Id = 1,
                UserId = 1
            });

            builder.Entity<Cart>().HasData(new Cart
            {
                Id = 2,
                UserId = 2
            });

            builder.Entity<Cart>().HasData(new Cart
            {
                Id = 3,
                UserId = 3
            });
        }
    }
}
