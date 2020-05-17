using Domain.Abstract;
using Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

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

        private GenericRepository<Image> _repository;
        private MainViewModel _viewModel;
        public IEnumerable<int> Articles { get; set; }

        public AddNewImageViewModel(GenericRepository<Image> repo,IEnumerable<int> articles, MainViewModel vm)
        {
            _repository = repo;
            Articles = articles;
            Image = new Image();
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

        private RelayCommand _addImageCommand;
        public RelayCommand AddImageCommand
        {
            get
            {
                if (_addImageCommand == null)
                {
                    _addImageCommand = new RelayCommand(p => AddImage(), x => ClothesId != 0);
                }

                return _addImageCommand;
            }
        }

        private List<byte[]> ImageToByteArray(List<string> filePath)
        {
            List<byte[]> files = new List<byte[]>();
            try
            {
                foreach (var file in filePath)
                {
                    files.Add(File.ReadAllBytes(file));
                }

                return files;
            }
            catch (IOException)
            {
                return null;
            }
        }

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

                    _repository.Add(newImage);
                    _repository.Save();
                    ObservableCollectionSingleton.GetInstance()?.ClothesImages.Add(newImage);
                }
            }
        }

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
    }
}