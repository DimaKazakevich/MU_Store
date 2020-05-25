using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using UnitedDirectManager.ObservableCollections;

namespace UnitedDirectManager.ViewModels
{
    public class OrdersViewModel : IPageViewModel, INotifyPropertyChanged
    {
        #region INPC
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion
        private OrdersObservableCollection _orders;

        public int Row { get; set; }

        public bool IsChecked { get; set; } = true;

        public string NavButtonName { get; } = "Orders";

        private IEnumerable<OrderDetails> _orderDetails;

        public IEnumerable<OrderDetails> OrderDeta
        {
            get
            {
                return _orderDetails;
            }
        }

        public OrdersViewModel(IOrderUnitOfWork repository, int row)
        {
            _orders = OrdersObservableCollection.GetInstance(repository);
            Row = row;
            _orderDetails = repository.OrderDetails.GetAll().ToList();
        }

        public ObservableCollection<Order> Orders
        {
            get
            {
                return _orders.Orders;
            }
            set
            {
                if (value != _orders.Orders)
                {
                    _orders.Orders = value;
                    OnPropertyChanged("Orders");
                }
            }
        }

        private Order _selectedItem;

        public Order SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                if (value != _selectedItem)
                {
                    _selectedItem = value;
                    OnPropertyChanged("SelectedItem");
                }
            }
        }
    }
}
