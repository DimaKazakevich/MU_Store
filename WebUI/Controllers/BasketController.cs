using Domain.Abstract;
using Domain.Entities;
using System.Linq;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class BasketController : Controller
    {
        private IProductUnitOfWork _repository;

        public BasketController(IProductUnitOfWork repository)
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
            Product wear = _repository.Products.GetAll().Where(item => item.Article == clothesID).FirstOrDefault();
            Size sizeName = wear.Sizes.First(x => x.SizeName == size);

            if (wear != null && sizeName != null)
            {
                basket.AddItem(wear, 1, size);
            }

            return PartialView("_AddToBasketPartial", basket);
        }

        public ActionResult AddToBasketWithoutSize(Basket basket, int clothesID)
        {
            Product wear = _repository.Products.GetAll().Where(item => item.Article == clothesID).FirstOrDefault();

            if (wear != null)
            {
                basket.AddItem(wear, 1);
            }

            return PartialView("_AddToBasketPartial", basket);
        }


        public void RemoveFromBasket(Basket basket, int clothesID)
        {
            Product wear = _repository.Products.GetAll().FirstOrDefault(item => item.Article == clothesID);

            if (wear != null)
            {
                basket.RemoveLine(wear);
            }
        }

        public void RemoveFromBasketWithSize(Basket basket, int clothesID, string size)
        {
            Product wear = _repository.Products.GetAll().FirstOrDefault(item => item.Article == clothesID);
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
                .Where(item => item.Product.Article == clothesID)
                .Where(item=>item.Size == size)
                .FirstOrDefault();

            if (line == null)
            {
                return null;
            }

            line.Quantity++;

            var result = new { quantity = line.Quantity, price = line.Product.Price };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DecrementClothesWithSize(Basket basket, int clothesID, string size)
        {
            BasketLine line = basket.Lines
                .Where(item => item.Product.Article == clothesID)
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
                basket.RemoveLine(line.Product);
            }

            var result = new { quantity = line.Quantity, price = line.Product.Price };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IncrementClothes(Basket basket, int clothesID)
        {
            BasketLine line = basket.Lines
                .Where(item => item.Product.Article == clothesID)
                .FirstOrDefault();
            
            if(line == null)
            {
                return null;
            }
                      
            line.Quantity++;

            var result = new { quantity = line.Quantity, price = line.Product.Price };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DecrementClothes(Basket basket, int clothesID)
        {
            BasketLine line = basket.Lines
                .Where(item => item.Product.Article == clothesID)
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
                basket.RemoveLine(line.Product);
            }

            var result = new { quantity = line.Quantity, price = line.Product.Price };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SizeDetails(int id)
        {
            ProductSizesViewModel wearSizes = new ProductSizesViewModel
            {
                Sizes = _repository.Products.GetAll().Where(x => x.Article == id).FirstOrDefault().Sizes.Select(x => x.SizeName),
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