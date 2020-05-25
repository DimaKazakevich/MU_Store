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

        public ProductsController()
        {
        }

        public ProductsController(IProductUnitOfWork repository)
        {
            _repository = repository;
        }

        public ViewResult List(string category, int page = 1)
        {
            ClothesListViewModel model = new ClothesListViewModel
            {
                Clothes = _repository.Products.GetAll().ToList()
                .Where(p => category == null || p.Category == category)
                .OrderBy(product => product.Article)
                .Skip((page - 1) * _pageSize)
                .Take(_pageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsOnPage = _pageSize,
                    TotalItems = category == null ? _repository.Products.GetAll().Count() :
                    _repository.Products.GetAll().Where(clothes => clothes.Category == category).Count()
                },
                CurrentCategory = category
            };
            return View(model);
        }

        public ViewResult ListAsc(string category, int page = 1)
        {
            ClothesListViewModel model = new ClothesListViewModel
            {
                Clothes = _repository.Products.GetAll().ToList()
                .Where(p => category == null || p.Category == category)
                .OrderBy(product => product.Price)
                .Skip((page - 1) * _pageSize)
                .Take(_pageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsOnPage = _pageSize,
                    TotalItems = category == null ? _repository.Products.GetAll().Count() :
                    _repository.Products.GetAll().Where(clothes => clothes.Category == category).Count()
                },
                CurrentCategory = category
            };
            return View("List",model);
        }

        public ViewResult ListDesc(string category, int page = 1)
        {
            ClothesListViewModel model = new ClothesListViewModel
            {
                Clothes = _repository.Products.GetAll().ToList()
                .Where(p => category == null || p.Category == category)
                .OrderByDescending(product => product.Price)
                .Skip((page - 1) * _pageSize)
                .Take(_pageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsOnPage = _pageSize,
                    TotalItems = category == null ? _repository.Products.GetAll().Count() :
                    _repository.Products.GetAll().Where(clothes => clothes.Category == category).Count()
                },
                CurrentCategory = category
            };
            return View("List",model);
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