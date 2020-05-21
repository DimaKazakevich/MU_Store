namespace UnitedDirectManager.ViewModels
{
    public class StatisticViewModel : IPageViewModel
    {
        public string NavButtonName { get; } = "Statistic";

        public int Row { get; set; }

        public StatisticViewModel(int row)
        {
            Row = row;
        }
    }
}
