using Domain.Entities;
using System.Data.Entity;

namespace Domain.Concrete
{
    public class EFDBContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Image> ClothesImages { get; set; }

        public DbSet<Size> Sizes { get; set; }

        //public DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }

        //public DbSet<IdentityUser> IdentityUser { get; set; }

        //public DbSet<AspNetRoles> AspNetRoles { get; set; }
    }
}