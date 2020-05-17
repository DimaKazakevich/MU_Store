using Domain.Abstract;
using Domain.Entities;
using System.Data.Entity;

namespace Domain.Concrete
{
    public class ProductSizesRepository : GenericRepository<Size>
    {
        public ProductSizesRepository(DbContext context) : base(context)
        { 
                _entities = context;
        }
    }
}
