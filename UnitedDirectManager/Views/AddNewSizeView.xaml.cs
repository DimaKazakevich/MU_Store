using System.Windows.Controls;
using UnitedDirectManager.ViewModels;

namespace UnitedDirectManager.Views
{
    public partial class AddNewSizeView : UserControl, IRightSideView
    {
        public AddNewSizeView(AddNewSizeViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
