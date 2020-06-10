using AppTaxi2020.Common.Models;
using AppTaxi2020.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTaxi2020.Web.Helpers
{
    public interface IConverterHelper
    {
        TaxiResponse ToTaxiResponse(TaxiEntity taxiEntity);
        TripResponse ToTripResponse(TripEntity tripEntity);
        UserResponse ToUserResponse(UserEntity userEntity);
    }
}
