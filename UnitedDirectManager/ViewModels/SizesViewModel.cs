using Domain.Abstract;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace UnitedDirectManager.ViewModels
{
    public class ProductSizesViewModel : IPageViewModel
    {
        public ProductSizesViewModel() { }

        private GenericRepository<Size> _repository;

        public ProductSizesViewModel(GenericRepository<Size> repository)
        {
            _repository = repository;
            Sizes = _repository.GetAll().ToList();
        }

        public IEnumerable<Size> Sizes { get; }

        public string NavButtonName { get; } = "Sizes";
    }
}
