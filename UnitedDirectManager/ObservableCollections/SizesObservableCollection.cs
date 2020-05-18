using Domain.Abstract;
using Domain.Entities;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace UnitedDirectManager.ObservableCollections
{
    public class SizesObservableCollection : INotifyPropertyChanged
    {
        #region INPC
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion

        private SizesObservableCollection(GenericRepository<Size> repository)
        {
            _productSizes = new ObservableCollection<Size>(repository.GetAll().ToList());
        }

        private static SizesObservableCollection _instance;

        public static SizesObservableCollection GetInstance(GenericRepository<Size> repository)
        {
            return _instance ?? (_instance = new SizesObservableCollection(repository));
        }

        public static SizesObservableCollection GetInstance()
        {
            if (_instance != null)
            {
                return _instance;
            }

            return null;
        }

        private ObservableCollection<Size> _productSizes;

        public ObservableCollection<Size> ProductSizes
        {
            get => _productSizes;
            set
            {
                _productSizes = value;
                OnPropertyChanged("ProductSizes");
            }
        }
    }
}
