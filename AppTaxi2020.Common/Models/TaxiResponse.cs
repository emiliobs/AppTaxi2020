using System;
using System.Collections.Generic;
using System.Text;

namespace AppTaxi2020.Common.Models
{
   public class TaxiResponse
    {
        public int Id { get; set; }

        public string Plaque { get; set; }

        public List<TripResponse> Trips { get; set; }

        public UserResponse User { get; set; }

    }
}
