using System.Windows;
using UnitedDirectManager.ViewModels;

namespace UnitedDirectManager
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class MainWindow : Window, ICloseable, IPageViewModel
    {
        public MainWindow(MainWindowViewModel clothesViewModel)
        {
            InitializeComponent();

            DataContext = clothesViewModel;
        }

        private void Main_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
