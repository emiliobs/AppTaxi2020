using Prism.Navigation;

namespace AppTaxi2020.Prison.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        public HomePageViewModel(INavigationService navigationService):base(navigationService)
        {
            Title = "Taxi Qualifier";

        }

        
    }
}
