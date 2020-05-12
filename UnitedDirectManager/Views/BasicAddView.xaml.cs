using System.Windows.Controls;
using UnitedDirectManager.ViewModels;

namespace UnitedDirectManager.Views
{
    /// <summary>
    /// Interaction logic for BasicAddView.xaml
    /// </summary>
    public partial class BasicAddView : UserControl, IRightSideView
    {
        public BasicAddView(MainWindowViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
