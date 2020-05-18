using Domain.Abstract;
using Domain.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using UnitedDirectManager.ObservableCollections;

namespace UnitedDirectManager.ViewModels
{
    public class AddNewImageViewModel : INotifyPropertyChanged
    {
        #region INPC
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion

        private GenericRepository<Image> _productRepository;
        private MainViewModel _viewModel;
        private readonly ObservableCollection<Product> _products;

        public ObservableCollection<Product> Articles
        {
            get => _products;
        }

        public AddNewImageViewModel(GenericRepository<Image> repo, MainViewModel vm)
        {
            _products = ProductsObservableCollection.GetInstance()?.Products;
            _productRepository = repo;
            Image = new Image();
            _viewModel = vm;
        }

        #region CancelAddViewCommand
        private RelayCommand _cancelAddViewCommand;

        public RelayCommand CancelAddViewCommand
        {
            get
            {
                return _cancelAddViewCommand ??  (_cancelAddViewCommand = new RelayCommand(p => CancelAddView()));
            }
        }

        private void CancelAddView()
        {
            _viewModel.CurrentAddView = _viewModel.AddViews[0];
        }
        #endregion

        #region AddImageCommand
        private RelayCommand _addImageCommand;
        public RelayCommand AddImageCommand
        {
            get
            {
                return _addImageCommand ?? (_addImageCommand = new RelayCommand(p => AddImage(), x => ClothesId != 0));
            }
        }

        private List<byte[]> _files = new List<byte[]>();

        private List<byte[]> ImageToByteArray(List<string> filePath)
        {
            _files.Clear();
            try
            {
                foreach (var file in filePath)
                {
                    _files.Add(File.ReadAllBytes(file));
                }

                return _files;
            }
            catch (IOException)
            {
                /// <summary>
                /// check null in AddImage method
                /// </summary>
                return null;
            }
        }

        /// <summary>
        /// Check null ImageToByteArray
        /// </summary>
        private void AddImage()
        {
            DefaultDialogService defaultDialog = new DefaultDialogService();
            if (defaultDialog.OpenFileDialog())
            {
                foreach (var file in ImageToByteArray(defaultDialog.FilePathes))
                {
                    Image newImage = new Image()
                    {
                        ClothesId = ClothesId,
                        ImageFile = file
                    };

                    _productRepository.Add(newImage);
                    _productRepository.Save();
                    ImagesObservableCollection.GetInstance()?.ProductImages.Add(newImage);
                }
            }
        }
        #endregion

        #region binding
        public Image Image { get; set; }

        public int ClothesId
        {
            get { return Image.ClothesId; }
            set
            {
                Image.ClothesId = value;
                OnPropertyChanged("ClothesId");
            }
        }
        #endregion
    }
}