using Domain.Abstract;
using Domain.Entities;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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

        public string NavButtonName { get; } = "Products";

        public ProductsObservableCollection _products;

        private IProductUnitOfWork _productUnitOfWork;

        public int Row { get; set; }

        public ProductsViewModel(IProductUnitOfWork repo, int row)
        {
            _products = ProductsObservableCollection.GetInstance(repo);
            _productUnitOfWork = repo;
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

        #region DeleteItemCommand
        private RelayCommand _deleteItemCommand;
        public RelayCommand DeleteItemCommand
        {
            get
            {
                if (_deleteItemCommand == null)
                {
                    _deleteItemCommand = new RelayCommand(
                        p => DeleteItem(), x => SelectedItem != null);
                }

                return _deleteItemCommand;
            }
        }

        public void DeleteItem()
        {
            foreach(var item in _productUnitOfWork.Images.GetAll().Where(x=>x.ClothesId == _selectedItem.Article))
            {
                _productUnitOfWork.Images.Delete(item);
                ImagesObservableCollection.GetInstance()?.ProductImages.Remove(item);
            }

            foreach (var item in _productUnitOfWork.Sizes.GetAll().Where(x => x.ClothesId == _selectedItem.Article))
            {
                _productUnitOfWork.Sizes.Delete(item);
                SizesObservableCollection.GetInstance()?.ProductSizes.Remove(item);
            }

            _productUnitOfWork.Products.Delete(_selectedItem);
            _productUnitOfWork.Products.Save();
            ProductsObservableCollection.GetInstance()?.Products.Remove(_selectedItem);
        }
        #endregion

        private Product _selectedItem;

        public Product SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                if (value != _selectedItem)
                {
                    _selectedItem = value;
                    OnPropertyChanged("SelectedItem");
                }
            }
        }
    }
}
