using Domain.Abstract;
using System.Collections.Generic;
using System.ComponentModel;
using UnitedDirectManager.ViewModels;

namespace UnitedDirectManager
{
    public class ViewManager : INotifyPropertyChanged
    {
        #region INPC
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion

        private static ViewManager _instance;

        public static ViewManager GetInstance()
        {
            if (_instance != null)
            {
                return _instance;
            }

            return null;
        }

        public static ViewManager GetInstance(ILoginUnitOfWork loginUnitOfWork, IProductUnitOfWork productUnitOfWork, IOrderUnitOfWork orderUnitOfWork, IOrderProcessor processor)
        {
            
            if (_instance == null)
            {
                _instance = new ViewManager(loginUnitOfWork, productUnitOfWork, orderUnitOfWork, processor);
            }
            return _instance;
        }

        private ViewManager(ILoginUnitOfWork loginUnitOfWork, IProductUnitOfWork productUnitOfWork, IOrderUnitOfWork orderUnitOfWork, IOrderProcessor processor)
        {
            PageViewModels.Add(new LoginViewModel(loginUnitOfWork));
            PageViewModels.Add(new MainViewModel(productUnitOfWork, orderUnitOfWork, loginUnitOfWork, processor));
            CurrentPageViewModel = PageViewModels[0];
        }

        private IPageViewModel _currentPageViewModel;
        private List<IPageViewModel> _pageViewModels;

        public List<IPageViewModel> PageViewModels
        {
            get
            {
                if (_pageViewModels == null)
                {
                    _pageViewModels = new List<IPageViewModel>();
                }

                return _pageViewModels;
            }
        }

        public IPageViewModel CurrentPageViewModel
        {
            get
            {
                return _currentPageViewModel;
            }
            set
            {
                if (_currentPageViewModel != value)
                {
                    _currentPageViewModel = value;
                    OnPropertyChanged("CurrentPageViewModel");
                }
            }
        }

        public void ChangeViewModel(bool check)
        {
            if(check)
            {
                CurrentPageViewModel = PageViewModels[1];
            }
        }
    }
}
