using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AppTaxi2020.Common.Models
{
    public class IncidentRequest : TripRequest
    {
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Remarks { get; set; }

    }
}
