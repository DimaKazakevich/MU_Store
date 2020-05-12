using Domain.Abstract;
using Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace UnitedDirectManager.ViewModels
{
    class ClothesViewModel : IPageViewModel, INotifyPropertyChanged
    {
        private IClothesRepository _repository;
        public IEnumerable<Wear> Wears { get; }

        public ClothesViewModel(IClothesRepository repository)
        {
            _repository = repository;
            Wears = _repository.Clothes.ToList();
        }
        
        #region INPC
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion

        public string NavButtonName { get; } = "Clothes";
    }
}
