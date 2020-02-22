using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppTaxi2020.Common.Services
{
    public class GeolocatorService : IGeolocatorService
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public async Task GetLocationAsync()
        {
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 20;
                var location = await locator.GetPositionAsync();
                Latitude = location.Latitude;
                Longitude = location.Latitude;
            }
            catch (Exception ex)
            {

                ex.ToString();
            }
        }
    }
}
