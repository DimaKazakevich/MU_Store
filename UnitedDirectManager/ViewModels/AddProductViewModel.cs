using Domain.Abstract;
using Domain.Entities;
using System.ComponentModel;
using UnitedDirectManager.ObservableCollections;
using UnitedDirectManager.Views;

namespace UnitedDirectManager.ViewModels
{
    public class AddProductViewModel : IPageViewModel, INotifyPropertyChanged, IRightSideView
    {
        public AddProductViewModel() { }

        #region INPC
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion

        private IProductUnitOfWork _productRepository;
        private MainViewModel _viewModel;

        public AddProductViewModel(IProductUnitOfWork repository, MainViewModel vm)
        {
            _productRepository = repository;
            _viewModel = vm;
        }

        #region CancelAddViewCommand
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
            _viewModel.CurrentAddView = _viewModel.AddViews[0];
        }
        #endregion

        #region SaveItemCommand
        private RelayCommand _saveItemCommand;
        public RelayCommand SaveItemCommand
        {
            get
            {
                return _saveItemCommand ?? (_saveItemCommand = new RelayCommand(p => SaveItem(), x => CheckTextBoxes()));
            }
        }

        private bool CheckTextBoxes()
        {
            if(!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Description) && 
                !string.IsNullOrEmpty(Category) && Price != 0)
            {
                return true;
            }

            return false;
        }
        
        private void SaveItem()
        {
            Product newProduct = new Product()
            {
                Name = Name,
                Description = Description,
                Category = Category,
                //Price = Price
            };

            _productRepository.Products.Add(newProduct);
            _productRepository.Products.Save();
            ProductsObservableCollection.GetInstance()?.Products.Add(newProduct);
            Name = string.Empty;
            Description = string.Empty;
            Category = string.Empty;
            Price = 0;
        }
        #endregion

        #region bindings
        public Product Product { get; set; } = new Product();

        public string Name
        {
            get { return Product.Name; }
            set { Product.Name = value; OnPropertyChanged("Name"); }
        }

        public string Description
        {
            get { return Product.Description; }
            set { Product.Description = value; OnPropertyChanged("Description"); }
        }

        public string Category
        {
            get { return Product.Category; }
            set { Product.Category = value; OnPropertyChanged("Category"); }
        }

        private decimal? lol;
        public decimal? Price
        {
            get { return lol; }
            set { lol = value; OnPropertyChanged("Price"); }
        }
        #endregion
    }
}
