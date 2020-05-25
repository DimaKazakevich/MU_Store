using Domain.Entities;

namespace Domain.Abstract
{
    public interface IOrderUnitOfWork
    {
        GenericRepository<Order> Orders { get; }

        GenericRepository<OrderDetails> OrderDetails { get; }
    }
}
