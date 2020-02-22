using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppTaxi2020.Common.Services
{
    public interface IGeolocatorService
    {
        double Latitude { get; set; }

        double Longitude { get; set; }

        Task GetLocationAsync();

    }
}
