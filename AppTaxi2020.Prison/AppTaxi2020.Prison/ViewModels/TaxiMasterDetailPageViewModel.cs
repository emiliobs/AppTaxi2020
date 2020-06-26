using AppTaxi2020.Common.Helpers;
using AppTaxi2020.Common.Models;
using AppTaxi2020.Prison.Helpers;
using Newtonsoft.Json;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AppTaxi2020.Prison.ViewModels
{
    public class TaxiMasterDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private UserResponse _user;

        public TaxiMasterDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            LoadMenus();
            LoadUser();
        }

        public UserResponse User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }



        public ObservableCollection<MenuItemViewModel> Menus { get; set; }

        private void LoadMenus()
        {
            List<Menu> menus = new List<Menu>
            {
                new Menu
                {
                    Icon = "ic_airport_shuttle",
                    PageName = "HomePage",
                    Title = Languages.NewTrip
                },
                new Menu
                {
                    Icon = "ic_local_taxi",
                    PageName = "TaxiHistoryPage",
                    Title = Languages.SeeTaxiHistory
                },
                new Menu
                {
                    Icon = "ic_people",
                    PageName = "GroupPage",
                    Title = Languages.AdminMyUserGroup
                },
                new Menu
                {
                    Icon = "ic_account_circle",
                    PageName = "ModifyUserPage",
                    Title = Languages.ModifyUser
                },
                new Menu
                {
                    Icon = "ic_report",
                    PageName = "ReportPage",
                    Title = Languages.ReportAnIncident
                },
                new Menu
                {
                    Icon = "ic_exit_to_app",
                    PageName = "LoginPage",
                    Title = Settings.IsLogin ? Languages.Logout : Languages.LogIn
                }

            };

            Menus = new ObservableCollection<MenuItemViewModel>(menus.Select(m => new MenuItemViewModel(_navigationService)
            {

                Icon = m.Icon,
                PageName = m.PageName,
                Title = m.Title,

            }).ToList());
        }

        private void LoadUser()
        {
            if (Settings.IsLogin)
            {
                User = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            }
        }

    }
}
