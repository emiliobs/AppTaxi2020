using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AppTaxi2020.Common.Models
{
    public class TripDetailRequest
    {
        [Required]
        public int TripId { get; set; }

        [MaxLength(500, ErrorMessage = "The {0} field must have {1} characters.")]
        public string Address { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

    }
}
