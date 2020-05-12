using Domain.Abstract;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;


namespace UnitedDirectManager.ViewModels
{
    public class ClothesImagesViewModel : IPageViewModel
    {
        private IClothesRepository _repository;
        public ClothesImagesViewModel(IClothesRepository repository)
        {
            _repository = repository;
            ClothesImages = _repository.ClothesImages.ToList();
        }

        public IEnumerable<ClothesImages> ClothesImages { get; }

        public string NavButtonName { get; } = "Images";
    }
}
