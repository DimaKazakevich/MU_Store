using Domain.Abstract;
using Domain.Concrete;
using Ninject;
using System.Data.Entity;
using System.Windows;
using UnitedDirectManager.Interfeces;
using UnitedDirectManager.ViewModels;
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
#region
//    // Run startup code first
//    base.OnStartup(e);

//// Create Login Window and Show it
//var login = new LoginDialog();
//var loginVm = new LoginViewModel();

//login.DataContext = loginVm;
//    login.ShowDialog();

//    // If login window didn't return true (login failed), exit application
//    if (!login.DialogResult.GetValueOrDefault())
//    {
//        Environment.Exit(0);
//    }

//    // Providing we have a successful login, start main application window
//    var app = new ShellView();
//var context = new ShellViewModel(loginVm.CurrentUser);
//app.DataContext = context;
//    app.Show();
#endregion