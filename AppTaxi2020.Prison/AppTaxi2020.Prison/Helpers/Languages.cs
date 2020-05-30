using AppTaxi2020.Prison.Interfaces;
using AppTaxi2020.Prison.Resources;
using Xamarin.Forms;

namespace AppTaxi2020.Prison.Helpers
{
    public static class Languages
    {
        static Languages()
        {
            System.Globalization.CultureInfo ci = Xamarin.Forms.DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            Culture = ci.Name;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string Culture { get; set; }

        public static string Accept => Resource.Accept;

        public static string ConnectionError => Resource.ConnectionError;

        public static string Error => Resource.Error;

        public static string PlaqueError1 => Resource.PlaqueError1;

        public static string PlaqueError2 => Resource.PlaqueError2;

        public static string TaxiHistory => Resource.TaxiHistory;


    }
}
