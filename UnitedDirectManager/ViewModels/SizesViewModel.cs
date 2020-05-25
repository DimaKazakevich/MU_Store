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

        private IProductUnitOfWork _productUnitOfWork;

        public int Row { get; set; }

        public ProductSizesViewModel(IProductUnitOfWork repo, int row)
        {
            _productSizes = SizesObservableCollection.GetInstance(repo);
            _productUnitOfWork = repo;
            Row = row;
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
            _productUnitOfWork.Sizes.Delete(_selectedItem);
            _productUnitOfWork.Sizes.Save();
            SizesObservableCollection.GetInstance()?.ProductSizes.Remove(_selectedItem);
        }
        #endregion

        private Size _selectedItem;

        public Size SelectedItem
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

