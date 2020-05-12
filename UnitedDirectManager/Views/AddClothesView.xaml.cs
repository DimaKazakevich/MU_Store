using System.Windows.Controls;
using UnitedDirectManager.ViewModels;

namespace UnitedDirectManager.Views
{
    /// <summary>
    /// Interaction logic for AddClothesView.xaml
    /// </summary>
    public partial class AddClothesView : UserControl, IRightSideView
    {
        public AddClothesView(MainWindowViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
