
using Android.App;
using Android.OS;

namespace AppTaxi2020.Prison.Droid
{
    [Activity(
         Theme = "@style/Theme.Splash",
         MainLauncher = true,
         NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            System.Threading.Thread.Sleep(1800);
            StartActivity(typeof(MainActivity));
        }


    }
}