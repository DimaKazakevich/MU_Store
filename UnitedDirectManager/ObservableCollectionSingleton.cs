using Domain.Abstract;
using Domain.Entities;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace UnitedDirectManager
{
    public class ObservableCollectionSingleton : INotifyPropertyChanged
    { 
        #region INPC
            public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion
    
        private static ObservableCollectionSingleton instance;

        public static ObservableCollectionSingleton GetInstance(GenericRepository<Image> repository)
        {
            if (instance == null)
            {
                instance = new ObservableCollectionSingleton(repository);
            }
            return instance;
        }

        public static ObservableCollectionSingleton GetInstance()
        {
            if (instance != null)
            {
                return instance;
            }
            else
            {
                return null;
            }
        }

        private ObservableCollection<Image> _clothesImages;

        public ObservableCollection<Image> ClothesImages
        {
            get => _clothesImages;
            set
            {
                _clothesImages = value;
                OnPropertyChanged("ClothesImages");
            }
        }
        private ObservableCollectionSingleton(GenericRepository<Image> repository)
        {
            _clothesImages = new ObservableCollection<Image>(repository.GetAll().ToList());
        }
    }
}

