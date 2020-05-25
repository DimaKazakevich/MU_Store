using Domain.Abstract;
using Domain.Entities;
using System.Collections.ObjectModel;
using System.ComponentModel;
using UnitedDirectManager.ObservableCollections;

namespace UnitedDirectManager.ViewModels
{
    public class ProductImagesViewModel : IPageViewModel, INotifyPropertyChanged
    {
        #region INPC
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion

        public ProductImagesViewModel() { }

        private ImagesObservableCollection _productImages;

        private IProductUnitOfWork _productUnitOfWork;

        public int Row { get; set; }

        public ProductImagesViewModel(IProductUnitOfWork repository, int row)
        {
             _productImages = ImagesObservableCollection.GetInstance(repository);
            _productUnitOfWork = repository;
            Row = row;
        }

        public ObservableCollection<Image> ProductImages
        {
            get
            {
                return _productImages.ProductImages;
            }
            set
            {
                if (value != _productImages.ProductImages)
                {
                    _productImages.ProductImages = value;
                    OnPropertyChanged("ProductImages");
                }
            }
        }

        public string NavButtonName { get; } = "Images";

        #region DeleteItemCommand
        private RelayCommand _deleteItemCommand;
        public RelayCommand DeleteItemCommand
        {
            get
            {
                if (_deleteItemCommand == null)
                {
                    _deleteItemCommand = new RelayCommand(
                        p => DeleteItem(), x=> SelectedItem != null);
                }

                return _deleteItemCommand;
            }
        }

        public void DeleteItem()
        {
            _productUnitOfWork.Images.Delete(_selectedItem);
            _productUnitOfWork.Images.Save();
            ImagesObservableCollection.GetInstance()?.ProductImages.Remove(_selectedItem);
        }
        #endregion

        private Image _selectedItem;

        public Image SelectedItem
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
