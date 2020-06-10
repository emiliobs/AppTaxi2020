using System;
using System.Collections.Generic;
using System.Text;

namespace AppTaxi2020.Common.Models
{
    public class MyTripsRequest
    {
        public string UserId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

    }
}
