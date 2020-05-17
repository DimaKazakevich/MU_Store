using Domain.Abstract;
using Domain.Concrete;
using Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace UnitedDirectManager.ViewModels
{
    public class ProductsViewModel : IPageViewModel, INotifyPropertyChanged
    {
        public ProductsViewModel() { }

        public IEnumerable<Product> Products { get; }

        private GenericRepository<Product> _repository;

        public ProductsViewModel(GenericRepository<Product> repository)
        {
            _repository = repository;
            Products = _repository.GetAll().ToList();
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
