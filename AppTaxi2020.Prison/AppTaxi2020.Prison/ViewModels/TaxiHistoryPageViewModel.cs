﻿using AppTaxi2020.Common.Models;
using AppTaxi2020.Common.Services;
using AppTaxi2020.Prison.Helpers;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AppTaxi2020.Prison.ViewModels
{
    public class TaxiHistoryPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private TaxiResponse _taxi;
        private DelegateCommand _checkPlaqueCommand;
        private List<TripItemViewModel> _details;
        private bool _isRunning;

        public TaxiHistoryPageViewModel(INavigationService navigationService, IApiService apiService):base(navigationService)
        {
            Title = Languages.TaxiHistory;
            this._navigationService = navigationService;
            this._apiService = apiService;
        }

        public List<TripItemViewModel> Details 
        {
            get => _details;
            set => SetProperty(ref _details, value); 
        }

        public bool IsRunning
        {
            get => _isRunning;

            set => SetProperty(ref _isRunning, value);
        }

        public TaxiResponse Taxi
        {
            get { return _taxi; }
            set => SetProperty(ref _taxi, value);
        }

        public string Plaque { get; set; } = "TPQ123";

        public DelegateCommand CheckPlaqueCommand => _checkPlaqueCommand ?? (_checkPlaqueCommand = new DelegateCommand(CheckPlaqueAsync));

        private async void CheckPlaqueAsync()
        {
            if (string.IsNullOrEmpty(Plaque))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error,Languages.PlaqueError1,Languages.Accept);
                return;
            }

            var regex = new Regex(@"^([A-Za-z]{3}\d{3})$");
            if (!regex.IsMatch(Plaque))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.PlaqueError2, Languages.Accept);
                return;
            }


            IsRunning = true;

            var url = App.Current.Resources["UrlAPI"].ToString();
            var connection = await _apiService.CheckConnectionAsync(url);
            if (!connection)
            {
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.ConnectionError, Languages.Accept);
                return;
            }
        
            var response = await _apiService.GetTaxiAsync(Plaque, url,"api","/Taxis");
            IsRunning = false;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            Taxi = (TaxiResponse)response.Result;

            Details = Taxi.Trips.Select(t =>  new TripItemViewModel(_navigationService) 
            {

                EndDate = t.EndDate,
                Id = t.Id,
                Qualification = t.Qualification,
                Remarks = t.Remarks,
                Source = t.Source,
                SourceLatitude = t.SourceLatitude,
                SourceLongitude = t.SourceLongitude,
                StartDate = t.StartDate,
                Target = t.Target,
                TargetLatitude = t.TargetLatitude,
                TargetLongitude = t.TargetLongitude,
                TripDetails = t.TripDetails,
                User = t.User


            }).ToList();


        }
    }
}
