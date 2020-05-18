using Domain.Abstract;
using Domain.Entities;
using System.Collections.ObjectModel;
using System.ComponentModel;


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

        public ProductImagesViewModel(GenericRepository<Image> repository)
        {
             _productImages = ImagesObservableCollection.GetInstance(repository);
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
                        p => DeleteItem());
                }

                return _deleteItemCommand;
            }
        }

        public void DeleteItem()
        {
            //_repository.Add(newImage);
            //_repository.SaveChanges();
            //ObservableCollectionSingleton.GetInstance()?.ClothesImages.Add(newImage);
        }
        #endregion
    }
}
