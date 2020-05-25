using Domain.Abstract;
using System.Windows;
using System.Windows.Input;

namespace UnitedDirectManager.Views
{
    public partial class AppView : Window
    {
        public AppView(ILoginUnitOfWork loginUnitOfWork, IProductUnitOfWork productUnitOfWork, IOrderUnitOfWork orderUnitOfWork, IOrderProcessor processor)
        {
            InitializeComponent();
            DataContext = ViewManager.GetInstance(loginUnitOfWork, productUnitOfWork, orderUnitOfWork, processor);
        }

        private void ContentControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch { }
        }
    }
}
