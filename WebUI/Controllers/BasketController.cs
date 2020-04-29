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

        #region AddToBasketOld
        /*public RedirectToRouteResult AddToBasket(Basket basket, int clothesID, string returnUrl)
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
                return RedirectToAction("Login", "Account", new { returnUrl });
            }
        }*/
        #endregion

        public ActionResult AddToBasketPartial(Basket basket, int clothesID)
        {
            Wear wear = _repository.Clothes.FirstOrDefault(item => item.Article == clothesID);

            if (wear != null)
            {
                basket.AddItem(wear, 1);
            }
            else
            {
                basket.Lines.FirstOrDefault(item => item.Wear == wear).Quantity++;
            }

            return PartialView("_AddToBasketPartial", basket);
        }


        public void RemoveFromBasket(Basket basket, int clothesID)
        {
            Wear wear = _repository.Clothes.FirstOrDefault(item => item.Article == clothesID);

            if (wear != null)
            {
                basket.RemoveLine(wear);
            }
        }

        public PartialViewResult Summary(Basket basket)
        {
            return PartialView(basket);
        }

        public JsonResult IncrementClothes(Basket basket, int clothesID)
        {
            BasketLine line = basket.Lines
                .Where(item => item.Wear.Article == clothesID)
                .FirstOrDefault();
                      
            line.Quantity++;

            var result = new { quantity = line.Quantity, price = line.Wear.Price };

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public JsonResult DecrementClothes(Basket basket, int clothesID)
        {
            BasketLine line = basket.Lines
                .Where(item => item.Wear.Article == clothesID)
                .FirstOrDefault();

            if (line == null)
            {
                return null;
            }

            if (line.Quantity > 1)
            {
                line.Quantity--;
            }
            else
            {
                line.Quantity = 0;
                basket.RemoveLine(line.Wear);
            }

            var result = new { quantity = line.Quantity, price = line.Wear.Price };
            return Json(result, JsonRequestBehavior.AllowGet);

        }
    }
}