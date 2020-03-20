using Domain.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class NavigationController : Controller
    {
        private IClothesRepository _repository;
        public NavigationController(IClothesRepository repository)
        {
            _repository = repository;
        }
        // GET: Navigation
        public PartialViewResult Menu()
        {
            IEnumerable<string> categories = _repository.Clothes
                .Select(wear => wear.Category)
                .Distinct()
                .OrderBy(x => x);
            return PartialView(categories);
        }
    }
}