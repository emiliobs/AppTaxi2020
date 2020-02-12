using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppTaxi2020.Web.Data.Entities
{
    public class TripDetailEntity
    {
        public int Id { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = false)]
        public DateTime Date { get; set; }    
       
        public DateTime DateLocal => Date.ToLocalTime();

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public TripEntity Trip { get; set; }

    }
}
