using Domain.Entities;
using System.Collections.Generic;

namespace WebUI.Models
{
    public class CheckoutViewModel
    {
        //public IdentityUser User { get; set; }

        public Basket Basket { get; set; }

        public ShippingDetails Shipping { get; set; }

        public IEnumerable<ShippingDetails> ShippingDetails { get; set; }

        public Order Order { get; set; }

        public OrderDetails OrderDetails { get; set; }

        public string ReturnUrl { get; set; }
    }
}