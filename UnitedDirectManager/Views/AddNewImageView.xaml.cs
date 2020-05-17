using System.Windows.Controls;
using UnitedDirectManager.ViewModels;

namespace UnitedDirectManager.Views
{
    public partial class AddNewImageView : UserControl, IRightSideView
    {
        public AddNewImageView(AddNewImageViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
