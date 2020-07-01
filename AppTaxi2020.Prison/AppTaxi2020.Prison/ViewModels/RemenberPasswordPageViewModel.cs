using AppTaxi2020.Common.Helpers;
using AppTaxi2020.Common.Models;
using AppTaxi2020.Common.Services;
using AppTaxi2020.Prison.Helpers;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTaxi2020.Prison.ViewModels
{
    public class RemenberPasswordPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private readonly IRegexHelper _regexHelper;

        private bool _isRunning;
        private bool _isEnabled;
        private DelegateCommand _recoverCommand;

        public RemenberPasswordPageViewModel(INavigationService navigationService, 
            IApiService apiService, IRegexHelper regexHelper):base(navigationService)
        {
            this._navigationService = navigationService;
            this._apiService = apiService;
            this._regexHelper = regexHelper;
            Title = Languages.PasswordRecover;
            IsEnabled = true;
        }

        public DelegateCommand RecoverCommand => _recoverCommand ?? (_recoverCommand = new DelegateCommand(RecoverAsyn));
        
        public string Email { get; set; }

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

        private async void RecoverAsyn()
        {
            bool isValid = await ValidateData();
            if (!isValid)
            {
                return;
            }

            var url = App.Current.Resources["UrlAPI"].ToString();
            var connection = await _apiService.CheckConnectionAsync(url);
            if (!connection)
            {
                IsRunning = false;
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert(Languages.Error,Languages.ConnectionError, Languages.Accept);
                return;
            }

            var request = new EmailRequest
            {
                Email = Email,
                CultureInfo = Languages.Culture,
            };

            IsRunning = true;
            IsEnabled = false;

            var response = await _apiService.RecoverPasswordAsync(url, "/api","/Account/RecoverPassword", request);

            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            await App.Current.MainPage.DisplayAlert(Languages.Ok, response.Message, Languages.Accept);
            await _navigationService.GoBackAsync();
        }

        private async Task<bool> ValidateData()
        {
            if (string.IsNullOrWhiteSpace(Email) || !_regexHelper.IsValidEmail(Email))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.EmailError, Languages.Accept);
                return false;
            }

            return true;
        }
    }
}
