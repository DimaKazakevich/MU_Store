using Domain.Abstract;
using Domain.Entities;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace UnitedDirectManager.ObservableCollections
{
    public class ProductsObservableCollection : INotifyPropertyChanged
    {
        #region INPC
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion

        private ProductsObservableCollection(IProductUnitOfWork repository)
        {
            _products = new ObservableCollection<Product>(repository.Products.GetAll().ToList());
        }

        private static ProductsObservableCollection _instance;

        public static ProductsObservableCollection GetInstance(IProductUnitOfWork repository)
        {
            return _instance ?? (_instance = new ProductsObservableCollection(repository));
        }

        public static ProductsObservableCollection GetInstance()
        {
            if (_instance != null)
            {
                return _instance;
            }

            return null;
        }

        private ObservableCollection<Product> _products;

        public ObservableCollection<Product> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged("Products");
            }
        }
    }
}
