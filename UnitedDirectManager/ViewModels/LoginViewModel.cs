using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;

namespace UnitedDirectManager.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private IClothesRepository _repository;

        public LoginViewModel(IClothesRepository repository)
        {
            _repository = repository;
            AspNetUserRoles = _repository.AspNetUserRoles.ToList();
            IdentityUser = _repository.IdentityUser.ToList();
            AspNetRoles = _repository.AspNetRoles.ToList();
            CloseWindowCommand = new RelayCommand(x => CloseWindow((ICloseable)x));
        }

        public IEnumerable<AspNetRoles> AspNetRoles { get; set; }
        public IEnumerable<IdentityUser> IdentityUser { get; set; }
        public IEnumerable<AspNetUserRoles> AspNetUserRoles { get; set; }

        #region INPC
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion

        private bool _wrongLoginOrPswVisibility;

        public bool WrongLoginOrPswVisibility
        {
            get { return _wrongLoginOrPswVisibility; }
            set 
            { 
                _wrongLoginOrPswVisibility = value;
                OnPropertyChanged("WrongLoginOrPswVisibility");
            }
        }

        private string _login;
        public string Login
        {
            get { return _login; }
            set
            {
                if (!string.Equals(_login, value))
                {
                    _login = value;
                    WrongLoginOrPswVisibility = false;
                    OnPropertyChanged("Login");
                }
            }
        }

        private RelayCommand _loginCommand;
        public RelayCommand LoginCommand
        {
            get
            {
                return _loginCommand ?? (_loginCommand = new RelayCommand(param => SingIn(param), param => LoginCanExecute(param))); 
            }
        }

        public bool LoginCanExecute(object param)
        {
            if(!string.IsNullOrEmpty(_login) && !string.IsNullOrEmpty(param.ToString()))
            {
                return true;
            }

            return false;
        }

        private static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] buffer4;
            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            byte[] src = Convert.FromBase64String(hashedPassword);
            if ((src.Length != 0x31) || (src[0] != 0))
            {
                return false;
            }
            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }
            return ByteArraysEqual(buffer3, buffer4);
        }

        private static bool ByteArraysEqual(byte[] b1, byte[] b2)
        {
            if (b1 == b2) return true;
            if (b1 == null || b2 == null) return false;
            if (b1.Length != b2.Length) return false;
            for (int i = 0; i < b1.Length; i++)
            {
                if (b1[i] != b2[i]) return false;
            }
            return true;
        }

        private void SingIn(object param)
        {
            var passwordBox = param as PasswordBox;

            if (passwordBox == null)
            {
                return;
            }
            var password = passwordBox.Password;

            var manager = IdentityUser.Where(user => user.Email == _login && VerifyHashedPassword(user.PasswordHash, password)).FirstOrDefault();
            if(manager == null)
            {
                WrongLoginOrPswVisibility = true;
                MessageBox.Show("Wrong logim or password.", "Login error", MessageBoxButton.OK, MessageBoxImage.Error,MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
                return;
            }

            var isex = AspNetUserRoles.Where(userRole => userRole.UserId == manager.UserId).FirstOrDefault();
            if(isex == null)
            {
                WrongLoginOrPswVisibility = true;
                return;
            }

            var lol = AspNetRoles.Where(role => role.UserId == isex.RoleId).FirstOrDefault();
            if(lol == null)
            {
                WrongLoginOrPswVisibility = true;
                return;
            }

            UnitedDirectManager.MainWindow mainWindow = new MainWindow(new MainWindowViewModel(_repository));
            mainWindow.Show();
            Application.Current.MainWindow.Close();
        }

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