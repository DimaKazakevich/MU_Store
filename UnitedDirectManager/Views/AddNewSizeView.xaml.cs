using System.Windows.Controls;
using UnitedDirectManager.ViewModels;

namespace UnitedDirectManager.Views
{
    /// <summary>
    /// Interaction logic for AddNewSizeView.xaml
    /// </summary>
    public partial class AddNewSizeView : UserControl, IRightSideView
    {
        public AddNewSizeView(MainWindowViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
