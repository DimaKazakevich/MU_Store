﻿using Domain.Abstract;
using Domain.Concrete;
using Ninject;
using System.Data.Entity;
using System.Windows;
using UnitedDirectManager.Interfeces;
using UnitedDirectManager.Views;

namespace UnitedDirectManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IKernel _container;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigureContainer();
            ComposeObjects();
            Current.MainWindow.Show();
        }

        private void ConfigureContainer()
        {
            _container = new StandardKernel();
            _container.Bind<DbContext>().To<EFDBContext>();
            _container.Bind(typeof(GenericRepository<>)).To(typeof(ProductRepository)).Named("Products");
            _container.Bind(typeof(GenericRepository<>)).To(typeof(ProductImagesRepository)).Named("Images");
            _container.Bind(typeof(GenericRepository<>)).To(typeof(ProductSizesRepository)).Named("Sizes");
            _container.Bind(typeof(GenericRepository<>)).To(typeof(UserRepository)).Named("Users");
            _container.Bind(typeof(GenericRepository<>)).To(typeof(ShippingDetailsRepository)).Named("Details");
            _container.Bind(typeof(GenericRepository<>)).To(typeof(OrderRepositpry)).Named("Orders");
            _container.Bind(typeof(GenericRepository<>)).To(typeof(OrderDetailsRepository)).Named("OrderDetails");
            _container.Bind(typeof(IOrderProcessor)).To(typeof(EmailOrderProcessor));
            _container.Bind(typeof(ILoginUnitOfWork)).To(typeof(LoginUnitOfWork));
            _container.Bind(typeof(IProductUnitOfWork)).To(typeof(ProductUnitOfWork));
            _container.Bind(typeof(IOrderUnitOfWork)).To(typeof(OrderUnitOfWork));
        }

        private void ComposeObjects()
        {
            Current.MainWindow = _container.Get<AppView>();
        }
    }
}