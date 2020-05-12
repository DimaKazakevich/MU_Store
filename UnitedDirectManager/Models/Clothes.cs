using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UnitedDirectManager.Models
{
	public class Clothes : INotifyPropertyChanged
    {
        private int _article;
        private string _name;
        private string _description;

        public string Name
        {
            get { return _name; }
            set
            {
            _name = value;
            OnPropertyChanged("Name");
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        public int Article
        {
            get { return _article; }
            set
            {
                _article = value;
                OnPropertyChanged("Article");
            }
        }

        private int _category;

        public int Category
        {
            get { return _category; }
            set { _category = value; OnPropertyChanged("Category"); }
        }

        private int _price;

        public int Price
        {
            get { return _price; }
            set { _price = value; OnPropertyChanged("Price"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
