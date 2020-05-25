using Domain.Abstract;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Media;
using UnitedDirectManager.Views;

namespace UnitedDirectManager.ViewModels
{
    public class StatisticRightViewModel : IRightSideView
    {
        public Func<ChartPoint, string> PointLabel { get; set; }

        public SeriesCollection Series { get; set; }

        private IOrderUnitOfWork _orderUnitOfWork;

        ChartValues<int> ChartValues = new ChartValues<int>();

        public StatisticRightViewModel(IOrderUnitOfWork orderUnitOfWork)
        {
            PointLabel = chartPoint =>
                string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

            _orderUnitOfWork = orderUnitOfWork;

            Series = new SeriesCollection()
            {
                 new PieSeries()
                 {
                    Title = "Sent",
                    Fill = MyColorForFill,
                    DataLabels = true,
                    LabelPoint = this.PointLabel,
                    StrokeThickness = 1,
                    Values = new ChartValues<int>
                    {
                        _orderUnitOfWork.Orders.GetAll().Where(x => x.Status == "Sent").Count()
                    }
                 },
                 new PieSeries()
                 {
                    Title = "In processing",
                    Fill = MyColorForPoint,
                    StrokeThickness = 1,
                    DataLabels = true,
                    LabelPoint = this.PointLabel,
                    Values = new ChartValues<int>
                    {
                        _orderUnitOfWork.Orders.GetAll().Where(x => x.Status == "In processing").Count()
                    }
                 }
            };

            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 1, 0);
            timer.IsEnabled = true;
            timer.Tick += (o, t) => Refresh(orderUnitOfWork);
            timer.Start();
        }

        private void Refresh(IOrderUnitOfWork orderUnitOfWork)
        {
            Series[0].Values.Clear();

            Series[0].Values.Add(orderUnitOfWork.Orders.GetAll().Where(x => x.Status == "Sent").Count());

            Series[1].Values.Clear();

            Series[1].Values.Add(orderUnitOfWork.Orders.GetAll().Where(x => x.Status == "In processing").Count());
        }

        public SolidColorBrush MyColorForFill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(117, 98, 128));
        public SolidColorBrush MyColorForStroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(174, 150, 130));
        public SolidColorBrush MyColorForPoint = new SolidColorBrush(System.Windows.Media.Color.FromRgb(203, 77, 63));
    }
}
