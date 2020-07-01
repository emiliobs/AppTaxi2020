using AppTaxi2020.Common.Helpers;
using AppTaxi2020.Common.Services;
using AppTaxi2020.Prison.ViewModels;
using AppTaxi2020.Prison.Views;
using Prism;
using Prism.Ioc;
using Syncfusion.Licensing;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace AppTaxi2020.Prison
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            SyncfusionLicenseProvider.RegisterLicense("Mjc3Mzk0QDMxMzgyZTMxMmUzMGNiYU1RU0dBVTIwcmp0Wk5GTnhYYmxhdE5GUG5PVVN5a1d0QVFuVlRIQWs9");
            InitializeComponent();

            //Device.SetFlags(new[] { "Shapes_Experimental", "MediaElement_Experimental" });

            await NavigationService.NavigateAsync("/TaxiMasterDetailPage/NavigationPage/HomePage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            
            containerRegistry.Register<IFilesHelper, FilesHelper>();
            containerRegistry.Register<IGeolocatorService, GeolocatorService>();
            containerRegistry.Register<IRegexHelper, RegexHelper>();
            containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>();
            containerRegistry.RegisterForNavigation<TaxiMasterDetailPage, TaxiMasterDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<TaxiHistoryPage, TaxiHistoryPageViewModel>();
            containerRegistry.RegisterForNavigation<GroupPage, GroupPageViewModel>();
            containerRegistry.RegisterForNavigation<ModifyUserPage, ModifyUserPageViewModel>();
            containerRegistry.RegisterForNavigation<ReportPage, ReportPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<TripDetailPage, TripDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<RegisterPage, RegisterPageViewModel>();
            containerRegistry.RegisterForNavigation<RemenberPasswordPage, RemenberPasswordPageViewModel>();
        }
    }
}
