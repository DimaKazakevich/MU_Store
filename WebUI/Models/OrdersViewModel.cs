using Domain.Entities;
using System.Collections.Generic;

namespace WebUI.Models
{
    public class OrdersViewModel
    {
        public IEnumerable<Order> Order { get; set; }

        public IEnumerable<OrderDetails> OrderDetails { get; set; }
    }
}