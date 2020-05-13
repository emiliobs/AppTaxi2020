using AppTaxi2020.Common.Models;


using AppTaxi2020.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTaxi2020.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        public TaxiResponse ToTaxiResponse(TaxiEntity taxiEntity)
        {
            return new TaxiResponse
            {
                Id = taxiEntity.Id,
                Plaque = taxiEntity.Plaque,
                Trips = taxiEntity.Trips?.Select(t => new TripResponse
                {
                    EndDate = t.EndDate,
                    Id = t.Id,
                    Qualification = t.Qualification,
                    Remarks = t.Remarks,
                    Source = t.Source,
                    SourceLatitude = t.SourceLatitude,
                    SourceLongitude = t.SourceLongitude,
                    StartDate = t.StartDate,
                    Target = t.Target,
                    TargetLatitude = t.TargetLatitude,
                    TargetLongitude = t.TargetLongitude,
                    TripDetails = t.TripDetails?.Select(td => new TripDetailResponse
                    {
                        Date = td.Date,
                        Id = td.Id,
                        Latitude = td.Latitude,
                        Longitude = td.Longitude
                    }).ToList(),
                    User = ToUserResponse(t.UserEntity)
                }).ToList(),
                User = ToUserResponse(taxiEntity.UserEntity)
            };
        }

        public TripResponse ToTripResponse(TripEntity tripEntity)
        {
            return new TripResponse
            {
                EndDate = tripEntity.EndDate,
                Id = tripEntity.Id,
                Qualification = tripEntity.Qualification,
                Remarks = tripEntity.Remarks,
                Source = tripEntity.Source,
                SourceLatitude = tripEntity.SourceLatitude,
                SourceLongitude = tripEntity.SourceLongitude,
                StartDate = tripEntity.StartDate,
                Target = tripEntity.Target,
                TargetLatitude = tripEntity.TargetLatitude,
                TargetLongitude = tripEntity.TargetLongitude,
                TripDetails = tripEntity.TripDetails?.Select(td => new TripDetailResponse {

                    Date = td.Date,
                    Id = td.Id,
                    Latitude = td.Latitude,
                    Longitude = td.Longitude
                }).ToList(),
                User = ToUserResponse(tripEntity.UserEntity),
                
            };
        }

        private UserResponse ToUserResponse(UserEntity user)
        {
            if (user == null)
            {
                return null;
            }

            return new UserResponse
            {
                Address = user.Address,
                Document = user.Document,
                Email = user.Email,
                FirstName = user.FirstName,
                Id = user.Id,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                PicturePath = user.PicturePath,
                UserType = user.UserType
            };
        }



    }

}
