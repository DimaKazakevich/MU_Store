using Domain.Abstract;
using Domain.Entities;
using System.Data.Entity;

namespace Domain.Concrete
{
    public class ShippingDetailsRepository : GenericRepository<ShippingDetails>
    {
        public ShippingDetailsRepository(DbContext context) : base(context)
        {
            _entities = context;
        }
    }
}
