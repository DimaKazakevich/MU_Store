using Domain.Abstract;
using Domain.Entities;
using System.Data.Entity;

namespace Domain.Concrete
{
    public class ProductImagesRepository : GenericRepository<Image>
    {
        public ProductImagesRepository(DbContext context) : base(context)
        {
            _entities = context;
        }
    }
}
