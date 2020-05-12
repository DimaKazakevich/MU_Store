using Domain.Abstract;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnitedDirectManager.Views;

namespace UnitedDirectManager.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
        }

        public MainWindowViewModel(IClothesRepository repository)
        {
            PageViewModels.Add(new ClothesImagesViewModel(repository));
            PageViewModels.Add(new ClothesViewModel(repository));
            PageViewModels.Add(new SizesViewModel(repository));
            CurrentPageViewModel = PageViewModels[0];
            CloseWindowCommand = new RelayCommand(x => CloseWindow((ICloseable)x));

            AddViews.Add(new BasicAddView(this));
            AddViews.Add(new SendEmailView());
            AddViews.Add(new AddClothesView(this));
            AddViews.Add(new AddNewSizeView(this));
            CurrentAddView = AddViews[0];
        }

        #region INPC
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion

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

            if (viewModel is ClothesViewModel)
            {

                CurrentAddView = AddViews.FirstOrDefault(vm => vm == AddViews[0]);
            }

            if (viewModel is SizesViewModel)
            {

                CurrentAddView = AddViews.FirstOrDefault(vm => vm == AddViews[0]);
            }

            CurrentPageViewModel = PageViewModels.FirstOrDefault(vm => vm == viewModel);
        }

        #region Right Side Bar
        private RelayCommand _changeAddViewCommad;
        private IRightSideView _currentAddView;
        private List<IRightSideView> _addViews;
        private RelayCommand _cancelAddViewCommand;

        public RelayCommand CancelAddViewCommand
        {
            get
            {
                if (_cancelAddViewCommand == null)
                {
                    _cancelAddViewCommand = new RelayCommand(
                            p => CancelAddView());
                }

                return _cancelAddViewCommand;
            }
        }

        private void CancelAddView()
        {
            CurrentAddView = AddViews.FirstOrDefault(vm => vm == AddViews[0]);
        }

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
            if (viewModel is ClothesViewModel)
            {
                CurrentAddView = AddViews.FirstOrDefault(vm => vm == AddViews[2]);
            }
            if (viewModel is SizesViewModel)
            {
                CurrentAddView = AddViews.FirstOrDefault(vm => vm == AddViews[3]);
            }
        }
        #endregion

        public RelayCommand _closeWindowCommand;

        public RelayCommand CloseWindowCommand { get; private set; }

        private void CloseWindow(ICloseable window)
        {
            if (window != null)
            {
                window.Close();
            }
        }
    }
}