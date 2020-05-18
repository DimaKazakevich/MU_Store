﻿using Domain.Abstract;
using Domain.Entities;
using System.Collections.ObjectModel;
using System.ComponentModel;
using UnitedDirectManager.ObservableCollections;

namespace UnitedDirectManager.ViewModels
{
    public class AddNewSizeViewModel : INotifyPropertyChanged
    {
        #region INPC
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion

        private GenericRepository<Size> _sizesRepository;
        private MainViewModel _viewModel;
        private readonly ObservableCollection<Product> _products;

        public ObservableCollection<Product> Articles 
        {
            get => _products;
        }

        public AddNewSizeViewModel(GenericRepository<Size> repo, MainViewModel vm)
        {
            _products = ProductsObservableCollection.GetInstance()?.Products;
            _sizesRepository = repo;
            Size = new Size();
            _viewModel = vm;
        }

        #region cancelCommand
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

        #region AddSizeCommand
        private RelayCommand _addSizeCommand;
        public RelayCommand AddSizeCommand
        {
            get
           {
                if (_addSizeCommand == null)
                {
                    _addSizeCommand = new RelayCommand(p => AddSize(), x => ClothesId != 0 && SizeName != null);
                }

                return _addSizeCommand;
            }
        }

        private void AddSize()
        {
            Size newSize = new Size()
            {
                ClothesId = ClothesId,
                SizeName = SizeName
            };

            _sizesRepository.Add(newSize);
            _sizesRepository.Save();
            SizesObservableCollection.GetInstance()?.ProductSizes.Add(newSize);
            SizeName = string.Empty;
        }
        #endregion

        #region bindings
        public Size Size { get; set; }

        public int ClothesId
        {
            get { return Size.ClothesId; }
            set
            {
                Size.ClothesId = value;
                OnPropertyChanged("ClothesId");
            }
        }

        public string SizeName
        {
            get { return Size.SizeName; }
            set
            {
                Size.SizeName = value;
                OnPropertyChanged("SizeName");
            }
        }
        #endregion
    }
}
