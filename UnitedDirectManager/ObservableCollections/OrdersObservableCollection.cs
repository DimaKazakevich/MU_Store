using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace UnitedDirectManager.ObservableCollections
{
    public class OrdersObservableCollection : INotifyPropertyChanged
    {
        #region INPC
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion

        private OrdersObservableCollection(IOrderUnitOfWork repository)
        {
            _orders = new ObservableCollection<Order>(repository.Orders.GetAll().ToList());

            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 1, 0);
            timer.IsEnabled = true;
            timer.Tick += (o, t) => Refresh(repository);
            timer.Start();
        }

        private void Refresh(IOrderUnitOfWork orderUnitOfWork)
        {
            _orders.Clear();
            foreach(var item in orderUnitOfWork.Orders.GetAll())
            {
                _orders.Add(item);
            }
        }

        private static OrdersObservableCollection _instance;

        public static OrdersObservableCollection GetInstance(IOrderUnitOfWork repository)
        {
            return _instance ?? (_instance = new OrdersObservableCollection(repository));
        }

        public static OrdersObservableCollection GetInstance()
        {
            if (_instance != null)
            {
                return _instance;
            }

            return null;
        }

        private ObservableCollection<Order> _orders;

        public ObservableCollection<Order> Orders
        {
            get => _orders;
            set
            {
                _orders = value;
                OnPropertyChanged("Orders");
            }
        }
    }
}
