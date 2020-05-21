using System.Windows;
using UnitedDirectManager.Interfeces;

namespace UnitedDirectManager
{
    public class WindowService : IWindowService
    {
        public void ShowWindow(object viewModel)
        {
            var win = new Window();
            win.DataContext = viewModel;
            win.Show();
        }
    }
}
