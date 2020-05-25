using Domain.Abstract;
using Domain.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class CheckoutController : Controller
    {
        private IOrderUnitOfWork _orderUnitOfWork;

        public CheckoutController(IOrderUnitOfWork orderUnitOfWork)
        {
            _orderUnitOfWork = orderUnitOfWork;
        }

        public ViewResult Index(Basket basket)
        {
            return View(new CheckoutViewModel
            {
                Basket = basket,
                ShippingDetails = new ShippingDetails(),
                Order = new Order()
            });
        }

        [HttpPost]
        public ViewResult AddNewOrder(Basket basket)
        {
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