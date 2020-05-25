using Domain.Abstract;
using Domain.Entities;
using System.Data.Entity;

namespace Domain.Concrete
{
    public class OrderRepositpry : GenericRepository<Order>
    {
        public OrderRepositpry(DbContext context) : base(context)
        {
            _entities = context;
        }
    }
}
