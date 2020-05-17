using Domain.Abstract;
using Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace UnitedDirectManager.ViewModels
{
    public class AddProductViewModel : IPageViewModel, INotifyPropertyChanged
    {
        public AddProductViewModel() { }

        #region INPC
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion

        private GenericRepository<Product> _repository;
        private MainViewModel _viewModel;
        public AddProductViewModel(GenericRepository<Product> repository, MainViewModel vm)
        {
            _repository = repository;
            Clothes = _repository.GetAll().ToList();
            _viewModel = vm;
        }

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

        public IEnumerable<Product> Clothes { get; set; }

        public Product Product { get; set; } = new Product();

        private RelayCommand _saveItemCommand;
        public RelayCommand SaveItemCommand
        {
            get
            {
                if (_saveItemCommand == null)
                {
                    _saveItemCommand = new RelayCommand(
                        p => SaveItem());
                }

                return _saveItemCommand;
            }
        }

        private void SaveItem()
        {
            //_repository.Add(Product);
            //_repository.SaveChanges();
        }

        public string Name
        {
            get { return Product.Name; }
            set { Product.Name = value; }
        }

        public string Description
        {
            get { return Product.Description; }
            set { Product.Description = value; }
        }

        public string Category
        {
            get { return Product.Category; }
            set { Product.Category = value; }
        }

        public decimal Price
        {
            get { return Product.Price; }
            set { Product.Price = value; }
        }
    }
}
