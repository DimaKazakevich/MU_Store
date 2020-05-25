using Domain.Abstract;
using Domain.Entities;
using System.Data.Entity;

namespace Domain.Concrete
{
    public class OrderDetailsRepository : GenericRepository<OrderDetails>
    {
        public OrderDetailsRepository(DbContext context) : base(context)
        {
            _entities = context;
        }
    }
}
