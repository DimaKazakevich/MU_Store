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

        public ActionResult AddToBasketWithSize(Basket basket, int clothesID, string size)
        {
            Wear wear = _repository.Clothes.Where(item => item.Article == clothesID).FirstOrDefault();
            Size sizeName = wear.Sizes.First(x => x.SizeName == size);

            if (wear != null && sizeName != null)
            {
                basket.AddItem(wear, 1, size);
            }

            return PartialView("_AddToBasketPartial", basket);
        }

        public ActionResult AddToBasketWithoutSize(Basket basket, int clothesID)
        {
            Wear wear = _repository.Clothes.Where(item => item.Article == clothesID).FirstOrDefault();

            if (wear != null)
            {
                basket.AddItem(wear, 1);
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

        public void RemoveFromBasketWithSize(Basket basket, int clothesID, string size)
        {
            Wear wear = _repository.Clothes.FirstOrDefault(item => item.Article == clothesID);
            Size sizeName = wear.Sizes.First(x => x.SizeName == size);

            if (wear != null && sizeName != null)
            {
                basket.RemoveLine(wear, size);
            }
        }

        public PartialViewResult Summary(Basket basket)
        {
            return PartialView(basket);
        }

        public JsonResult IncrementClothesWithSize(Basket basket, int clothesID, string size)
        {
            BasketLine line = basket.Lines
                .Where(item => item.Wear.Article == clothesID)
                .Where(item=>item.Size == size)
                .FirstOrDefault();

            if (line == null)
            {
                return null;
            }

            line.Quantity++;

            var result = new { quantity = line.Quantity, price = line.Wear.Price };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DecrementClothesWithSize(Basket basket, int clothesID, string size)
        {
            BasketLine line = basket.Lines
                .Where(item => item.Wear.Article == clothesID)
                .Where(item => item.Size == size)
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

        public JsonResult IncrementClothes(Basket basket, int clothesID)
        {
            BasketLine line = basket.Lines
                .Where(item => item.Wear.Article == clothesID)
                .FirstOrDefault();
            
            if(line == null)
            {
                return null;
            }
                      
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

        public ActionResult SizeDetails(int id)
        {
            WearSizesViewModel wearSizes = new WearSizesViewModel
            {
                Sizes = _repository.Clothes.Where(x => x.Article == id).FirstOrDefault().Sizes.Select(x => x.SizeName),
                ClothesID = id
            };
            if(wearSizes.Sizes.Count() > 0)
            {
                return PartialView(wearSizes);
            }

            return null;
        }
    }
}