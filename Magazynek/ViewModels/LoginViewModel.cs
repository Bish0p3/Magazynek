using Magazynek.Helpers;
using Magazynek.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Magazynek.ViewModels
{
    internal class LoginViewModel : ObservableObject
    {

        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }


        public LoginViewModel ()
        {

        }


        private ICommand _loginCommand;
        public ICommand LoginCommand
        {
            get { return _loginCommand = _loginCommand ?? new Command(LoginAction); }
        }


        public void LoginAction()
        {
            DatabaseService databaseService = new();

            if(databaseService.Login(Username, Password))
            {
                Application.Current.MainPage = new AppShell();
            }
            else
            {
               
            }
            
            
        }
    }
}
