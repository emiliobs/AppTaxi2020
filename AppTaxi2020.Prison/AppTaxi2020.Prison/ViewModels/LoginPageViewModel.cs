using AppTaxi2020.Common.Helpers;
using AppTaxi2020.Common.Models;
using AppTaxi2020.Common.Services;
using AppTaxi2020.Prison.Helpers;
using Newtonsoft.Json;
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
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private bool _isRunning;
        private bool _isEnabled;
        private string _password;
        private DelegateCommand _loginCommand;
        private DelegateCommand _registerCommand;


        public LoginPageViewModel(INavigationService navigationService , IApiService apiService)  :base(navigationService)
        {
            Title = Languages.LogIn;
            IsEnabled = true;
          
            this._navigationService = navigationService;
            this._apiService = apiService;
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

            IsRunning = true;
            IsEnabled = false;

            var url = App.Current.Resources["UrlAPI"].ToString();
            var connection = await _apiService.CheckConnectionAsync(url);
            if (!connection)
            {
                IsRunning = false;
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ConnectionError, Languages.Accept);
                return;
            }


            var tokenRequest = new TokenRequest
            {
                Password = Password,
                Username = Email,
            };


            var response = await _apiService.GetTokenAsync(url, "Account", "/CreateToken", tokenRequest);

            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.LoginError, Languages.Accept);
                Password = string.Empty;
                return;
            }


            var token = (TokenResponse)response.Result;
            var emailRequest = new EmailRequest
            {
                CultureInfo = Languages.Culture,
                Email = Email,
            };

            var responseGetUserByEmail = await _apiService.GetUserByEmail(url, "api", "/Account/GetUserByEmail", "bearer", token.Token, emailRequest);
            var userResponse = (UserResponse)responseGetUserByEmail.Result;

            //Datos en persistencia.
            Settings.User = JsonConvert.SerializeObject(userResponse);
            Settings.Token = JsonConvert.SerializeObject(token);
            Settings.IsLogin = true;

            await _navigationService.NavigateAsync("/TaxiMasterDetailPage/NavigationPage/HomePage");
            Password = string.Empty;

            IsRunning = false;
            IsEnabled = true;
        }

        private async void RegisterAsync()
        {
          

        }



      
    }
}
