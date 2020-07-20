using Domain.Abstract;
using Domain.Entities;
using Ninject;

namespace Domain.Concrete
{
    public class OrderUnitOfWork : IOrderUnitOfWork
    {
        private GenericRepository<Order> _orderRepository;
        private GenericRepository<OrderDetails> _orderDetailsRepository;
        private GenericRepository<ShippingDetails> _shippingDetails;

        public OrderUnitOfWork([Named("Orders")] GenericRepository<Order> orderRepo,
                                [Named("OrderDetails")] GenericRepository<OrderDetails> orderDetailsRepo,
                                [Named("Details")] GenericRepository<ShippingDetails> shippingDetails)
        {
            _orderRepository = orderRepo;
            _orderDetailsRepository = orderDetailsRepo;
            _shippingDetails = shippingDetails;
        }

        public GenericRepository<Order> Orders => _orderRepository;

        public GenericRepository<OrderDetails> OrderDetails => _orderDetailsRepository;

        public GenericRepository<ShippingDetails> ShippingDetails => _shippingDetails;
    }
}
