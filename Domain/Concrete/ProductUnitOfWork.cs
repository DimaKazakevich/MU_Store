using Domain.Abstract;
using Domain.Entities;
using Ninject;

namespace Domain.Concrete
{
    public class ProductUnitOfWork : IProductUnitOfWork
    {       
        private GenericRepository<Size> _sizesRepository;
        private GenericRepository<Image> _imagesRepository;
        private GenericRepository<Product> _productsRepository;

        public ProductUnitOfWork([Named("Products")] GenericRepository<Product> productsRepo,
                                [Named("Images")] GenericRepository<Image> imagesRepo,
                                [Named("Sizes")] GenericRepository<Size> sizesRepo)
        {
            _sizesRepository = sizesRepo;
            _imagesRepository = imagesRepo;
            _productsRepository = productsRepo;
        }

        public GenericRepository<Size> Sizes => _sizesRepository;

        public GenericRepository<Image> Images => _imagesRepository;

        public GenericRepository<Product> Products => _productsRepository;
    }
}
