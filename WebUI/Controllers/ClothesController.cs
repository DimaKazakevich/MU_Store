using System.Linq;
using System.Web.Mvc;
using Domain.Abstract;using Domain.Entities;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class ClothesController : Controller
    {
        // GET: Clothes
        private IClothesRepository _repository;

        public int _pageSize = 2;

        public ClothesController()
        {
        }

        public ClothesController(IClothesRepository repository)
        {
            _repository = repository;
        }

        public ViewResult List(string category, int page = 1)
        {
            ClothesListViewModel model = new ClothesListViewModel
            {
                Clothes = _repository.Clothes
                .Where(p => category == null || p.Category == category)
                .OrderBy(wear => wear.Article)
                .Skip((page - 1) * _pageSize)
                .Take(_pageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsOnPage = _pageSize,
                    TotalItems = category == null ? _repository.Clothes.Count() :
                    _repository.Clothes.Where(clothes => clothes.Category == category).Count()
                },
                CurrentCategory = category
            };
            return View(model);
        }

        [HttpGet]
        [ActionName("clothes-details")]
        public ActionResult ClothesDetails(string name)
        {
            Wear wear = _repository.Clothes.FirstOrDefault(item => item.Name == name);
            return View("ClothesDetails", wear);
        }
    }
}