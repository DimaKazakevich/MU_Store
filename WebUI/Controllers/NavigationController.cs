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
        private IItemsRepository _repository;
        public NavigationController(IItemsRepository repository)
        {
            _repository = repository;
        }
        // GET: Navigation
        public PartialViewResult Menu()
        {            
            IEnumerable<string> categories = _repository.Clothes
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
                Clothes = _repository.Clothes
                .Where(x =>
            CultureInfo.CurrentCulture.CompareInfo.IndexOf
            (x.Name,searchString ,CompareOptions.IgnoreCase) >= 0).ToList(),


            PagingInfo = new PagingInfo
                {
                    CurrentPage = 1,
                    ItemsOnPage = _repository.Clothes.Count(),
                    TotalItems = _repository.Clothes.Count()
                },
            };

            ViewData["searchString"] = searchString;

            return View(viewName: "~/Views/Clothes/List.cshtml", model);
        }
    }
}