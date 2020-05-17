using Domain.Abstract;
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
            _container.Bind(typeof(IDialogService)).To(typeof(DefaultDialogService));
        }

        private void ComposeObjects()
        {
            Current.MainWindow = _container.Get<MainView>();
        }
    }
}
