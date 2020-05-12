using Domain.Abstract;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace UnitedDirectManager.ViewModels
{
    public class SizesViewModel : IPageViewModel
    {
        private IClothesRepository _repository;
        public SizesViewModel(IClothesRepository repository)
        {
            _repository = repository;
            Sizes = _repository.Sizes.ToList();
        }

        public IEnumerable<Size> Sizes { get; }

        public string NavButtonName { get; } = "Sizes";
    }
}
