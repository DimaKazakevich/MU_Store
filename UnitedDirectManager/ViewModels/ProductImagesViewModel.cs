using Domain.Abstract;
using Domain.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;


namespace UnitedDirectManager.ViewModels
{
    public class ProductImagesViewModel : IPageViewModel, INotifyPropertyChanged
    {
        private ObservableCollectionSingleton observable;

        public ProductImagesViewModel() { }

        public ProductImagesViewModel(GenericRepository<Image> repository)
        {
             observable = ObservableCollectionSingleton.GetInstance(repository);
        }

        public ObservableCollection<Image> ClothesImages
        {
            get
            {
                return observable.ClothesImages;
            }
            set
            {
                if (value != observable.ClothesImages)
                {
                    observable.ClothesImages = value;
                    OnPropertyChanged("ClothesImages");
                }
            }
        }

        public string NavButtonName { get; } = "Images";

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

        #region INPC
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion
    }
}
