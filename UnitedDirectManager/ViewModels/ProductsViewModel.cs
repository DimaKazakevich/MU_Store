using Domain.Abstract;
using Domain.Entities;
using System.Collections.ObjectModel;
using System.ComponentModel;
using UnitedDirectManager.ObservableCollections;

namespace UnitedDirectManager.ViewModels
{
    public class ProductsViewModel : IPageViewModel, INotifyPropertyChanged
    {
        #region INPC
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion

        public ProductsViewModel() { }

        public ProductsObservableCollection _products;

        public int Row { get; set; }

        public ProductsViewModel(IProductUnitOfWork repo, int row)
        {
            _products = ProductsObservableCollection.GetInstance(repo);
            Row = row;
        }
       
        public ObservableCollection<Product> Products
        {
            get
            {
                return _products.Products;
            }
            set
            {
                if (value != _products.Products)
                {
                    _products.Products = value;
                    OnPropertyChanged("Products");
                }
            }
        }

        public string NavButtonName { get; } = "Products";
    }
}
