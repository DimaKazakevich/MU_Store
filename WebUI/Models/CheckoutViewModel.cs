using Domain.Entities;

namespace WebUI.Models
{
    public class CheckoutViewModel
    {

        public Basket Basket { get; set; }

        public ShippingDetails ShippingDetails { get; set; }

        public Order Order { get; set; }

        public OrderDetails OrderDetails { get; set; }
    }
}