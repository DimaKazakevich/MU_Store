using Domain.Abstract;
using Domain.Concrete;
using Ninject;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            _kernel = kernelParam;

            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            //Mock<IClothesRepository> mock = new Mock<IClothesRepository>();

            //mock.Setup(m => m.Clothes).Returns(new List<Wear>
            //{
            //    new Wear { Name = "T-shirt", Price = 300, },
            //    new Wear { Name = "Baseball cap", Price = 200}
            //});

            //_kernel.Bind<IClothesRepository>().ToConstant(mock.Object);

            //_kernel.Bind<IItemsRepository>().To<ItemsRepository>();
        }
    }
}