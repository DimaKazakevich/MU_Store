namespace UnitedDirectManager.ViewModels
{
    public class OrdersViewModel : IPageViewModel
    {
        public int Row { get; set; }

        public bool IsChecked { get; set; } = true;

        public string NavButtonName { get; } = "Orders";

        public OrdersViewModel(int row)
        {
            Row = row;
        }
    }
}
