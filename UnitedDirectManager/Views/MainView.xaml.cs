using System.Windows;
using UnitedDirectManager.ViewModels;

namespace UnitedDirectManager.Views
{
    public partial class MainView : Window, ICloseable, IPageViewModel
    {
        public MainView(MainViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void Main_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
