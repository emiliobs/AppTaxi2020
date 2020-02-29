using AppTaxi2020.Common.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppTaxi2020.Prison.ViewModels
{
    public class TripDetailPageViewModel : ViewModelBase
    {
        public TripDetailPageViewModel(INavigationService navigationService):base(navigationService)
        {
            Title = "Trip Details";
        }

        private TripResponse _trip;

        public TripResponse Trip
        {
            get => _trip;
            set => SetProperty(ref _trip, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("trip"))
            {
                Trip = parameters.GetValue<TripResponse>("trip");
            }

        }

    }
}
