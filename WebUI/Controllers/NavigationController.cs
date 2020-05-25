using Domain.Abstract;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class NavigationController : Controller
    {
        private IProductUnitOfWork _repository;
        public NavigationController(IProductUnitOfWork repository)
        {
            _repository = repository;
        }
        // GET: Navigation
        public PartialViewResult Menu()
        {            
            IEnumerable<string> categories = _repository.Products.GetAll()
            .Select(wear => wear.Category)
            .Distinct()
            .OrderBy(x => x).ToList();
            return PartialView(categories);
        }

        [HttpGet]
        public ViewResult Search(string searchString)
        {
            ClothesListViewModel model = new ClothesListViewModel
            {
                Clothes = _repository.Products.GetAll()
                .Where(x =>
            CultureInfo.CurrentCulture.CompareInfo.IndexOf
            (x.Name,searchString ,CompareOptions.IgnoreCase) >= 0).ToList(),


            PagingInfo = new PagingInfo
                {
                    CurrentPage = 1,
                    ItemsOnPage = _repository.Products.GetAll().Count(),
                    TotalItems = _repository.Products.GetAll().Count()
                },
            };

            ViewData["searchString"] = searchString;

            return View(viewName: "~/Views/Products/List.cshtml", model);
        }
    }
}