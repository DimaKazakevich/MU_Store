using Domain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Domain.Concrete
{
    public class EFDBContext : DbContext
    {
        public DbSet<Wear> Clothes { get; set; }

        public DbSet<ClothesImages> ClothesImages { get; set; }
    }
}
