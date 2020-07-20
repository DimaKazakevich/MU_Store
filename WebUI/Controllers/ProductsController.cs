using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Domain.Abstract;
using Domain.Entities;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class ProductsController : Controller
    {
        private IProductUnitOfWork _repository;

        public int _pageSize = 50;

        public ProductsController() { }

        public ProductsController(IProductUnitOfWork repository)
        {
            _repository = repository;
        }

        public ViewResult List(string category, int page = 1)
        {
            ClothesListViewModel model = new ClothesListViewModel
            {
                Clothes = _repository.Products.GetAll()
                                              .ToList()
                                              .Where(p => category == null || p.Category == category.Replace("-", "&"))
                                              .Skip((page - 1) * _pageSize)
                                              .Take(_pageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsOnPage = _pageSize,
                    TotalItems = category == null ? _repository.Products.GetAll().Count() :
                                                _repository.Products.GetAll().Where(clothes => clothes.Category == category.Replace("-","&")).Count()
                }
            };

            if(category != null)
            {
                model.CurrentCategory = category.Replace("-", "&");
            }

            return View(model);
        }

        [ActionName("heroes")]
        public ViewResult FindHeroesTshirts()
        {
            ClothesListViewModel model = new ClothesListViewModel
            {
                PagingInfo = new PagingInfo
                {
                    CurrentPage = 1,
                    ItemsOnPage = _repository.Products.GetAll().Count(),
                    TotalItems = _repository.Products.GetAll().Count()
                },
                CurrentCategory = "Heroes T-shirts"
            };

            model.Clothes = _repository.Products.GetAll()
                            .Where(x => CultureInfo.CurrentCulture.CompareInfo
                                                                .IndexOf(x.Name, "hero", CompareOptions.IgnoreCase) >= 0)
                            .ToList();

            return View(viewName: "~/Views/Products/List.cshtml", model);
        }

        [ActionName("home-kit")]
        public ViewResult GetHomeKit()
        {
            ClothesListViewModel model = new ClothesListViewModel
            {
                PagingInfo = new PagingInfo
                {
                    CurrentPage = 1,
                    ItemsOnPage = _repository.Products.GetAll().Count(),
                    TotalItems = _repository.Products.GetAll().Count()
                },
                CurrentCategory = "Home Kit"
            };

            model.Clothes = _repository.Products.GetAll()
                            .Where(x => CultureInfo.CurrentCulture.CompareInfo
                                                                .IndexOf(x.Name, "home", CompareOptions.IgnoreCase) >= 0)
                            .ToList();

            return View(viewName: "~/Views/Products/List.cshtml", model);
        }

        [ActionName("new-training")]
        public ViewResult GetAllTraining()
        {
            ClothesListViewModel model = new ClothesListViewModel
            {
                PagingInfo = new PagingInfo
                {
                    CurrentPage = 1,
                    ItemsOnPage = _repository.Products.GetAll().Count(),
                    TotalItems = _repository.Products.GetAll().Count()
                },
                CurrentCategory = "Training Wear"
            };

            model.Clothes = _repository.Products.GetAll()
                            .Where(x => CultureInfo.CurrentCulture.CompareInfo
                                                                .IndexOf(x.Name, "training", CompareOptions.IgnoreCase) >= 0)
                            .ToList();

            return View(viewName: "~/Views/Products/List.cshtml", model);
        }

        [ActionName("new-arrivals")]
        public ViewResult FindNewArrivals()
        {
            ClothesListViewModel model = new ClothesListViewModel
            {
                PagingInfo = new PagingInfo
                {
                    CurrentPage = 1,
                    ItemsOnPage = _repository.Products.GetAll().Count(),
                    TotalItems = _repository.Products.GetAll().Count()
                },
                Clothes = _repository.Products.GetAll().Where(x => x.Article >= 53 && x.Article <= 60).ToList(),
                CurrentCategory = "Hot New Arrivals"
            };

            return View(viewName: "~/Views/Products/List.cshtml", model);
        }

        [HttpGet]
        [ActionName("product-details")]
        public ActionResult ProductDetails(string name)
        {
            Product product = _repository.Products.GetAll().FirstOrDefault(item => item.Name == name);
            return View("ProductDetails", product);
        }
    }
}