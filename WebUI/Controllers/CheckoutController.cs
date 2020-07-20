using Domain.Abstract;
using Domain.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    [Route("checkout")]
    public class CheckoutController : Controller
    {
        private IOrderUnitOfWork _orderUnitOfWork;

        public CheckoutController(IOrderUnitOfWork orderUnitOfWork)
        {
            _orderUnitOfWork = orderUnitOfWork;
        }

        [ActionName("confirmation")]
        public ActionResult AddShippingDetails(Basket basket, CheckoutViewModel details, string returnUrl)
        {
            details.Shipping = new ShippingDetails();
            details.Basket = basket;

            if(returnUrl != null)
            {
                return Redirect(returnUrl);
            }

            return View(details);
        }

        [HttpPost]
        [ActionName("confirmed")]
        public ViewResult AddNewOrder(Basket basket, CheckoutViewModel details)
        {
            ShippingDetails shippingDetails = new ShippingDetails()
            {
                FirstName = details.Shipping.FirstName,
                LastName = details.Shipping.LastName,
                Country = details.Shipping.Country,
                Town = details.Shipping.Town,
                AddresLine = details.Shipping.AddresLine,
                Postcode = details.Shipping.Postcode,
                UserId = User.Identity.GetUserId()
            };

            _orderUnitOfWork.ShippingDetails.Add(shippingDetails);
            _orderUnitOfWork.ShippingDetails.Save();

            Order order = new Order()
            {
                DateTime = DateTime.Now,
                TotalCost = basket.ComputeTotalValue(),
                Status = "In processing",
                UserId = User.Identity.GetUserId()
            };

            _orderUnitOfWork.Orders.Add(order);
            _orderUnitOfWork.Orders.Save();

            foreach(var item in basket.Lines)
            {
                _orderUnitOfWork.OrderDetails.Add(new OrderDetails 
                { 
                    OrderId = order.Id, 
                    ProductId = item.Product.Article, 
                    Quantity = item.Quantity,
                    Size = item.Size
                });
            }
            _orderUnitOfWork.OrderDetails.Save();

            basket.Clear();

            return View();
        }
    }
}