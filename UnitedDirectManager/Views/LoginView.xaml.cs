using System.Windows;
using UnitedDirectManager.ViewModels;

namespace UnitedDirectManager.Views
{
    public partial class LoginView : Window
    {
        public LoginView(LoginViewModel loginViewModel)
        {
            InitializeComponent();
            DataContext = loginViewModel;
        }
    }
}
