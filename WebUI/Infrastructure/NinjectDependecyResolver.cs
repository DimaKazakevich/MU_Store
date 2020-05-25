using Domain.Abstract;
using Domain.Concrete;
using Ninject;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Mvc;

namespace WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _container;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            _container = kernelParam;

            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return _container.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.GetAll(serviceType);
        }

        private void AddBindings()
        {
            _container.Bind<DbContext>().To<EFDBContext>();
            _container.Bind(typeof(GenericRepository<>)).To(typeof(ProductRepository)).Named("Products");
            _container.Bind(typeof(GenericRepository<>)).To(typeof(ProductImagesRepository)).Named("Images");
            _container.Bind(typeof(GenericRepository<>)).To(typeof(ProductSizesRepository)).Named("Sizes");
            _container.Bind(typeof(GenericRepository<>)).To(typeof(UserRepository)).Named("Users");
            _container.Bind(typeof(GenericRepository<>)).To(typeof(OrderRepositpry)).Named("Orders");
            _container.Bind(typeof(GenericRepository<>)).To(typeof(OrderDetailsRepository)).Named("OrderDetails");
            _container.Bind(typeof(ILoginUnitOfWork)).To(typeof(LoginUnitOfWork));
            _container.Bind(typeof(IProductUnitOfWork)).To(typeof(ProductUnitOfWork));
            _container.Bind(typeof(IOrderUnitOfWork)).To(typeof(OrderUnitOfWork));
        }
    }
}