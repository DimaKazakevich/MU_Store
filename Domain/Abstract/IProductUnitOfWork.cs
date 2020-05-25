using Domain.Entities;

namespace Domain.Abstract
{
    public interface IProductUnitOfWork
    {
        GenericRepository<Size> Sizes { get; }
        GenericRepository<Image> Images { get; }
        GenericRepository<Product> Products { get; }
    }
}
