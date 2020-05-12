using Domain.Entities;
using System.Data.Entity;

namespace Domain.Concrete
{
    public class EFDBContext : DbContext
    {
        public DbSet<Wear> Clothes { get; set; }

        public DbSet<ClothesImages> ClothesImages { get; set; }

        public DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }

        public DbSet<IdentityUser> IdentityUser { get; set; }

        public DbSet<AspNetRoles> AspNetRoles { get; set; }

        public DbSet<Size> Sizes { get; set; }
    }
}