using Domain.Abstract;
using Domain.Entities;
using Ninject;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnitedDirectManager.Views;

namespace UnitedDirectManager.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region INPC
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion

        public MainViewModel() { }

        public MainViewModel([Named("Products")] GenericRepository<Product> productsRepo, 
                                [Named("Images")] GenericRepository<Image> imagesRepo,
                                [Named("Sizes")] GenericRepository<Size> sizesRepo)
        {
            PageViewModels.Add(new ProductImagesViewModel(imagesRepo));
            PageViewModels.Add(new ProductsViewModel(productsRepo));
            PageViewModels.Add(new ProductSizesViewModel(sizesRepo));
            CurrentPageViewModel = PageViewModels[0];
            
            AddViews.Add(new BasicAddView(this));
            AddViews.Add(new SendEmailView(this));
            AddViews.Add(new AddNewImageView(new AddNewImageViewModel(imagesRepo, this)));
            AddViews.Add(new AddNewSizeView(new AddNewSizeViewModel(sizesRepo, this)));
            AddViews.Add(new AddNewItemView(new AddProductViewModel(productsRepo, this)));
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