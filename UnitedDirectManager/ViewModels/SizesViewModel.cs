using Domain.Abstract;
using Domain.Entities;
using System.Collections.ObjectModel;
using System.ComponentModel;
using UnitedDirectManager.ObservableCollections;

namespace UnitedDirectManager.ViewModels
{
    public class ProductSizesViewModel : IPageViewModel, INotifyPropertyChanged
    {
        #region INPC
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion

        public ProductSizesViewModel() { }

        private SizesObservableCollection _productSizes;

        public ProductSizesViewModel(GenericRepository<Size> repo)
        {
            _productSizes = SizesObservableCollection.GetInstance(repo);
        }

        public ObservableCollection<Size> ProductSizes
        {
            get
            {
                return _productSizes.ProductSizes;
            }
            set
            {
                if (value != _productSizes.ProductSizes)
                {
                    _productSizes.ProductSizes = value;
                    OnPropertyChanged("ProductImages");
                }
            }
        }

        public string NavButtonName { get; } = "Sizes";
    }
}
