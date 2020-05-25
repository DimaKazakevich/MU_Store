using Domain.Abstract;
using Domain.Entities;
using Ninject;

namespace Domain.Concrete
{
    public class OrderUnitOfWork : IOrderUnitOfWork
    {
        private GenericRepository<Order> _orderRepository;
        private GenericRepository<OrderDetails> _orderDetailsRepository;

        public OrderUnitOfWork([Named("Orders")] GenericRepository<Order> orderRepo,
                                [Named("OrderDetails")] GenericRepository<OrderDetails> orderDetailsRepo)
        {
            _orderRepository = orderRepo;
            _orderDetailsRepository = orderDetailsRepo;
        }

        public GenericRepository<Order> Orders => _orderRepository;

        public GenericRepository<OrderDetails> OrderDetails => _orderDetailsRepository;
    }
}
