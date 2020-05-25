using Domain.Abstract;
using Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using UnitedDirectManager.Login;

namespace UnitedDirectManager.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged, IPageViewModel
    {
        #region INPC
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion

        private ILoginUnitOfWork _loginUnitOfWork;
        public IEnumerable<AspNetRoles> AspNetRoles { get; set; }
        public IEnumerable<IdentityUser> IdentityUser { get; set; }
        public IEnumerable<AspNetUserRoles> AspNetUserRoles { get; set; }

        public LoginViewModel(ILoginUnitOfWork loginUnitOfWork)
        {
            CloseWindowCommand = new RelayCommand(x => CloseWindow((ICloseable)x));
            _loginUnitOfWork = loginUnitOfWork;
            IdentityUser = _loginUnitOfWork.Users.GetAll().ToList();
        }

        #region binding
        private string _login;
        public string Login
        {
            get { return _login; }
            set
            {
                if (!string.Equals(_login, value))
                {
                    _login = value;
                    OnPropertyChanged("Login");
                }
            }
        }
        #endregion

        #region LoginCommand
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

        private void SingIn(object param)
        {
            var passwordBox = param as PasswordBox;

            if (passwordBox == null)
            {
                return;
            }
            var password = passwordBox.Password;

            var manager = IdentityUser.Where(user => user.Email == _login && VerifyPassword.VerifyHashedPassword(user.PasswordHash, password)).FirstOrDefault();

            if (manager == null)
            {
                MessageBox.Show("Wrong login or password.", "Login error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
                return;
            }


            #region
            //var isRoleExist = AspNetUserRoles.Where(userRole => userRole.UserId == manager.UserId).FirstOrDefault();
            //if (isRoleExist == null)
            //{
            //    MessageBox.Show("You do not have access to the application.", "Login error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
            //    return;
            //}

            //var concreteRole = AspNetRoles.Where(role => role.UserId == isRoleExist.RoleId).FirstOrDefault();
            //if (concreteRole == null)
            //{
            //    MessageBox.Show("You do not have access to the application.", "Login error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
            //    return;
            //}
            #endregion

            ///<summary>
            ///if login is successful pass true
            ///</summary>
            ViewManager.GetInstance().ChangeViewModel(true);
        }
        #endregion

        #region CloseWindowCommand
        public RelayCommand _closeWindowCommand;

        public RelayCommand CloseWindowCommand { get; private set; }

        private void CloseWindow(ICloseable window)
        {
            if (window != null)
            {
                window.Close();
            }
        }
        #endregion
    }
}