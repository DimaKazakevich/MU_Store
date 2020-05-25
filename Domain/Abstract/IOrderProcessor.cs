using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Abstract
{
    public interface IOrderProcessor
    {
        void ProcessOrder(string email, IEnumerable<OrderDetails> details);
    }
}
