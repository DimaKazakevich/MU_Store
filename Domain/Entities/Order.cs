using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Orders")]
    public class Order : INotifyPropertyChanged
    {
        #region INPC
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion

        public int Id { get; set; }

        public string UserId { get; set; }

        public DateTime DateTime { get; set; }

        public decimal TotalCost { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }

        private string _status;

        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
                OnPropertyChanged("Status");
            }
        }
    }
}
