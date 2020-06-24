using AppTaxi2020.Prison.Helpers;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppTaxi2020.Prison.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private bool _isRunning;
        private bool _isEnabled;
        private string _password;
        private DelegateCommand _loginCommand;
        private DelegateCommand _registerCommand;


        public LoginPageViewModel(INavigationService navigationService)  :base(navigationService)
        {
            Title = Languages.LogIn;
            IsEnabled = true;     
        }

        public DelegateCommand LoginCommand => _loginCommand ?? (_loginCommand = new DelegateCommand(LoginAsync));
        public DelegateCommand RegisterCommand => _registerCommand ?? (_registerCommand = new DelegateCommand(RegisterAsync));

        public bool IsRunning 
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        public string Email { get; set; }

        public string Password 
        {
            get => _password;
            set => SetProperty(ref _password,value);
        }

        private async void LoginAsync()
        {
            if (string.IsNullOrWhiteSpace(Email))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.EmailError, Languages.Accept);
                return;
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.Password, Languages.Accept);
                return;
            }
        }

        private async void RegisterAsync()
        {

            return;

        }



      
    }
}
