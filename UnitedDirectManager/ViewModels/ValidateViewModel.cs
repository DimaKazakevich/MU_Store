using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitedDirectManager.ViewModels
{
    public abstract class ValidateViewModel : IDataErrorInfo, INotifyPropertyChanged
    {
        #region INPC
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion

        private string _error;

        public string Error
        {
            get => _error;
            private set
            {
                if (value != _error)
                {
                    _error = value;
                    OnPropertyChanged("Error");
                }
            }
        }

        public string this[string columnName] => Error = Validate(columnName);

        protected abstract string Validate(string columnName);
    }
}
