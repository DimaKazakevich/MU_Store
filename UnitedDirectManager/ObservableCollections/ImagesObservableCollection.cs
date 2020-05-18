using Domain.Abstract;
using Domain.Entities;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace UnitedDirectManager
{
    public class ImagesObservableCollection : INotifyPropertyChanged
    { 
        #region INPC
            public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion

        private ImagesObservableCollection(GenericRepository<Image> repository)
        {
            _productImages = new ObservableCollection<Image>(repository.GetAll().ToList());
        }

        private static ImagesObservableCollection _instance;

        public static ImagesObservableCollection GetInstance(GenericRepository<Image> repository)
        {
            return _instance ?? (_instance = new ImagesObservableCollection(repository));       
        }

        public static ImagesObservableCollection GetInstance()
        {
            if (_instance != null)
            {
                return _instance;
            }

            return null;
        }

        private ObservableCollection<Image> _productImages;

        public ObservableCollection<Image> ProductImages
        {
            get => _productImages;
            set
            {
                _productImages = value;
                OnPropertyChanged("ProductImages");
            }
        }

    }
}

