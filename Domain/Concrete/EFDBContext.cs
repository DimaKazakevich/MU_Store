using Domain.Entities;
using System.Data.Entity;

namespace Domain.Concrete
{
    class EFDBContext : DbContext
    {
        public DbSet<Wear> Clothes { get; set; }
    }
}
