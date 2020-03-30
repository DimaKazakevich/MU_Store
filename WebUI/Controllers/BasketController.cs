using Domain.Abstract;
using Domain.Entities;
using System.Linq;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class BasketController : Controller
    {
        private IClothesRepository _repository;

        public BasketController(IClothesRepository repository)
        {
            _repository = repository;
        }

        public ViewResult Index(Basket basket, string returnUrl)
        {
            return View(new BasketIndexViewModel
            {
                Basket = basket,
                ReturnUrl = returnUrl
            });
        }
        public RedirectToRouteResult AddToBasket(Basket basket, int clothesID, string returnUrl)
        {
            if (Request.IsAuthenticated)
            {
                Wear wear = _repository.Clothes.FirstOrDefault(item => item.Article == clothesID);

                if (wear != null)
                {
                    basket.AddItem(wear, 1);
                }
                return RedirectToAction("Index", new { returnUrl });
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public RedirectToRouteResult RemoveFromBasket(Basket basket, int clothesID, string returnUrl)
        {
            Wear wear = _repository.Clothes.FirstOrDefault(item => item.Article == clothesID);

            if (wear != null)
            {
                basket.RemoveLine(wear);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult Summary(Basket basket)
        {
            return PartialView(basket);
        }
    }
}