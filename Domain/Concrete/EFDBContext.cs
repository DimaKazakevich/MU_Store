using Domain.Entities;
using System.Data.Entity;

namespace Domain.Concrete
{
    public class EFDBContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Image> ClothesImages { get; set; }

        public DbSet<Size> Sizes { get; set; }

        public DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }

        public DbSet<IdentityUser> IdentityUser { get; set; }

        public DbSet<AspNetRoles> AspNetRoles { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetails> OrdesDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityUser>().Ignore(c => c.AccessFailedCount)
                                               .Ignore(c => c.Claims)
                                               .Ignore(c => c.LockoutEnabled)
                                               .Ignore(c => c.LockoutEndDateUtc)
                                               .Ignore(c => c.PhoneNumber)
                                               .Ignore(c => c.Logins)
                                               .Ignore(c => c.PhoneNumberConfirmed)
                                               .Ignore(c => c.TwoFactorEnabled);
        }
    }
}