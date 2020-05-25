using Domain.Abstract;
using LiveCharts;
using LiveCharts.Wpf;
using Ninject.Infrastructure.Language;
using System;
using System.Linq;
using System.Windows.Media;

namespace UnitedDirectManager.ViewModels
{
    public class StatisticViewModel : IPageViewModel
    {
        public string NavButtonName { get; } = "Statistic";

        public int Row { get; set; }

        private ChartValues<int> CountOrdersPerDay;

        public StatisticViewModel(int row, IOrderUnitOfWork orderUnitOfWork)
        {
            Row = row;

            CountOrdersPerDay = new ChartValues<int>();

            var countOrdersPerDayEnumerable = orderUnitOfWork.Orders.GetAll().OrderBy(y => y.DateTime.Date)
                                                                             .GroupBy(x => x.DateTime.Date)
                                                                             .Select(g => new { Count = g.Count() }).ToEnumerable();
            

            foreach (var item in countOrdersPerDayEnumerable)
            {
                CountOrdersPerDay.Add(item.Count);
            }

            //var start = DateTime.Now.StartOfWeek();
            //var end = DateTime.Now.EndOfWeek();
            //.Where(x => x.Date >= start && x.Date <= end)

            Labels = orderUnitOfWork.Orders.GetAll().OrderBy(y=>y.DateTime.Date)
                                                    .Select(x=>x.DateTime.Date.ToString("dd/MM/yyyy"))
                                                    .Distinct().ToArray();
            
            SeriesCollection = new SeriesCollection()
            {
            new LineSeries
            {
            Values = CountOrdersPerDay,
            PointGeometry = DefaultGeometries.Circle,
            PointGeometrySize = 10,
            PointForeground = MyColorForPoint
            }};

            YFormatter = value => value.ToString("C");

            ((LineSeries)SeriesCollection[0]).Stroke = MyColorForStroke;
            ((LineSeries)SeriesCollection[0]).Fill = MyColorForFill;

            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 1, 0);
            timer.IsEnabled = true;
            timer.Tick += (o, t) => Refresh(orderUnitOfWork);
            timer.Start();
        }

        private void Refresh(IOrderUnitOfWork orderUnitOfWork)
        {
            SeriesCollection[0].Values.Clear();

            var countOrdersPerDayEnumerable = orderUnitOfWork.Orders.GetAll().OrderBy(y => y.DateTime.Date)
                                                                             .GroupBy(x => x.DateTime.Date)
                                                                             .Select(g => new { Count = g.Count() }).ToEnumerable();

            foreach (var item in countOrdersPerDayEnumerable)
            {
                SeriesCollection[0].Values.Add(item.Count);
            }
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        public SolidColorBrush MyColorForFill = new SolidColorBrush(Color.FromRgb(117, 98, 128));
        public SolidColorBrush MyColorForStroke = new SolidColorBrush(Color.FromRgb(174, 150, 130));
        public SolidColorBrush MyColorForPoint = new SolidColorBrush(Color.FromRgb(203, 77, 63));
    }
}
