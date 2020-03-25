using Domain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Domain.Concrete
{
    class EFDBContext : DbContext
    {
        public DbSet<Wear> Clothes { get; set; }
    }
}
