using Domain.Abstract;
using Domain.Entities;
using System.Data.Entity;

namespace Domain.Concrete
{
    public class ProductRepository : GenericRepository<Product>
    {
        public ProductRepository(DbContext context) : base(context)
        {
            _entities = context;
        }
    }
}
