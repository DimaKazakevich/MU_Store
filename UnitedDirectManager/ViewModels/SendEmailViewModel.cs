using Domain.Abstract;
using Domain.Concrete;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using UnitedDirectManager.ObservableCollections;
using UnitedDirectManager.Views;

namespace UnitedDirectManager.ViewModels
{
    public class SendEmailViewModel : IRightSideView
    {
        private RelayCommand _sendEmailCommand;
        public RelayCommand SendEmailCommand
        {
            get
            {
                if (_sendEmailCommand == null)
                {
                    _sendEmailCommand = new RelayCommand(p => SendEmail(), x => CheckIfSelectedAndStatus());
                }

                return _sendEmailCommand;
            }
        }

        private bool CheckIfSelectedAndStatus()
        {
            if(_orderViemModel.SelectedItem != null && _orderViemModel.SelectedItem.Status == "In processing")
            {
                return true;
            }

            return false;
        }

        private OrdersViewModel _orderViemModel;
        private IEnumerable<IdentityUser> _users;
        private IEnumerable<OrderDetails> _orderDetails;
        private IEnumerable<Order> _orders;
        private IOrderProcessor _orderProcessor;
        private IOrderUnitOfWork _reposiroty;

        public SendEmailViewModel(ILoginUnitOfWork loginUnitOfWork, IOrderUnitOfWork orders,
                                  OrdersViewModel viewModel, IOrderProcessor processor)
        {
            _orderViemModel = viewModel;
            _users = loginUnitOfWork.Users.GetAll().ToList();
            _orderDetails = orders.OrderDetails.GetAll().ToList();
            _orders = orders.Orders.GetAll().ToList();
            _orderProcessor = processor;
            _reposiroty = orders;
        }

        private void SendEmail()
        {
            var email = _users.Where(x => x.UserId == _orderViemModel.SelectedItem.UserId).Select(p => p.Email).First();
            var orderDetails = _orderDetails.Where(x => x.OrderId == _orderViemModel.SelectedItem.Id);
            _orderProcessor.ProcessOrder(email, orderDetails);
            var temp = _orders.Where(x => x.Id == _orderViemModel.SelectedItem.Id).First().Status = "Sent";
            var item = _orders.Where(x => x.Id == _orderViemModel.SelectedItem.Id).First();
            item.Status = "Sent";
            _reposiroty.Orders.Edit(item);
            _reposiroty.Orders.Save();
            var item1 = OrdersObservableCollection.GetInstance().Orders.Where(x => x.Id == _orderViemModel.SelectedItem.Id).First();
            item1.Status = "Sent";
        }

    }
}
