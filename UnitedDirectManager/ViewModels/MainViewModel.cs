using Domain.Abstract;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnitedDirectManager.Views;

namespace UnitedDirectManager.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged, IPageViewModel, IRightSideView
    {
        #region INPC
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion

        public MainViewModel(IProductUnitOfWork productUnitOfWork)
        {
            PageViewModels.Add(new OrdersViewModel(0));
            PageViewModels.Add(new ProductImagesViewModel(productUnitOfWork,1));
            PageViewModels.Add(new ProductsViewModel(productUnitOfWork, 2));
            PageViewModels.Add(new ProductSizesViewModel(productUnitOfWork,3));
            PageViewModels.Add(new StatisticViewModel(4));
            CurrentPageViewModel = PageViewModels[0];

            
            AddViews.Add(this);
            AddViews.Add(new SendEmailViewModel());
            AddViews.Add(new AddNewImageViewModel(productUnitOfWork, this));
            AddViews.Add(new AddNewSizeViewModel(productUnitOfWork, this));
            AddViews.Add(new AddProductViewModel(productUnitOfWork, this));
            CurrentAddView = AddViews[0];

            CloseWindowCommand = new RelayCommand(x => CloseWindow((ICloseable)x));
        }

        #region ChangePageCommand
        private RelayCommand _changePageCommand;
        private IPageViewModel _currentPageViewModel;
        private List<IPageViewModel> _pageViewModels;

        public RelayCommand ChangePageCommand
        {
            get
            {
                if (_changePageCommand == null)
                {
                    _changePageCommand = new RelayCommand(
                        p => ChangeViewModel((IPageViewModel)p),
                        p => p is IPageViewModel);
                }

                return _changePageCommand;
            }
        }

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

        private void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
            {
                PageViewModels.Add(viewModel);
            }

            if (viewModel is ProductsViewModel)
            {
                CurrentAddView = AddViews[0];
            }

            if (viewModel is ProductSizesViewModel)
            {
                CurrentAddView = AddViews[0];
            }

            if (viewModel is ProductImagesViewModel)
            {
                CurrentAddView = AddViews[0];
            }

            if (viewModel is OrdersViewModel)
            {
                CurrentAddView = AddViews[1];
            }

            if (viewModel is StatisticViewModel)
            {
                CurrentAddView = null;
            }

            CurrentPageViewModel = PageViewModels.FirstOrDefault(vm => vm == viewModel);
        }
        #endregion

        #region Right Side Bar
        private RelayCommand _changeAddViewCommad;
        private IRightSideView _currentAddView;
        private List<IRightSideView> _addViews;

        public RelayCommand ChangeAddViewCommand
        {
            get
           {
                if (_changeAddViewCommad == null)
                {
                    _changeAddViewCommad = new RelayCommand(p => ChangeView((IPageViewModel)p));
                }

                return _changeAddViewCommad;
            }
        }

        public List<IRightSideView> AddViews
        {
            get
            {
                if (_addViews == null)
                {
                    _addViews = new List<IRightSideView>();
                }

                return _addViews;
            }
        }

        public IRightSideView CurrentAddView
        {
            get
            {
                return _currentAddView;
            }
            set
            {
                if (_currentAddView != value)
                {
                    _currentAddView = value;
                    OnPropertyChanged("CurrentAddView");
                }
            }
        }

        private void ChangeView(IPageViewModel viewModel)
        {
            if (viewModel is ProductsViewModel)
            {
                CurrentAddView = AddViews[4];
            }
            if (viewModel is ProductSizesViewModel)
            {
                CurrentAddView = AddViews[3];
            }
            if (viewModel is ProductImagesViewModel)
            {
                CurrentAddView = AddViews[2];
            }
        }
        #endregion

        #region CloseWindowCommand
        public RelayCommand _closeWindowCommand;

        public RelayCommand CloseWindowCommand { get; private set; }

        private void CloseWindow(ICloseable window)
        {
            if (window != null)
            {
                window.Close();
            }
        }
        #endregion
    }
}