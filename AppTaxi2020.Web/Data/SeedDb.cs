using AppTaxi2020.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTaxi2020.Web.Data
{
    
    public class SeedDb
    {
       
        private readonly AppDataContext _dataContext;

        public SeedDb(AppDataContext dataContext)
        {
           
            _dataContext = dataContext;
           
        }

        public async Task SeedAsync()
        {
            await _dataContext.Database.EnsureCreatedAsync();
            await CheckTaxiAsync();
        }

        private async Task CheckTaxiAsync()
        {
            if (!_dataContext.Taxis.Any())
            {
                _dataContext.Taxis.Add(new TaxiEntity 
                {
                   Plaque = "CGQ123",
                   Trips = new List<TripEntity>() 
                   {
                       new TripEntity
                       { 
                           StartDate = DateTime.UtcNow, 
                           EndDate = DateTime.UtcNow.AddMinutes(30),
                           Qualification =4.5f,
                           Source = "Whitechapel",
                           Target = "London Centre",
                           Remarks = "Excellent Service",
                           
                       } ,

                        new TripEntity
                       {
                           StartDate = DateTime.UtcNow,
                           EndDate = DateTime.UtcNow.AddMinutes(30),
                           Qualification = 3.5f,
                           Source = "London Bridge",
                           Target = "Whitechapel",
                           Remarks = "Good Service",
                       } ,
                   },
                });

                _dataContext.Taxis.Add(new TaxiEntity
                {
                    Plaque = "OTU785",
                    Trips = new List<TripEntity>()
                   {
                       new TripEntity
                       {
                           StartDate = DateTime.UtcNow,
                           EndDate = DateTime.UtcNow.AddMinutes(30),
                           Qualification = 2.5f,
                           Source = "Pure Gym Shoreditch",
                           Target = "Tower Bridge",
                           Remarks = "Bad Service",
                       } ,

                        new TripEntity
                       {
                           StartDate = DateTime.UtcNow,
                           EndDate = DateTime.UtcNow.AddMinutes(30),
                           Qualification = 2.5f,
                           Source = "stratford",
                           Target = "Westminster",
                           Remarks = "Well Service",
                       } ,
                   },
                });
            }

            await _dataContext.SaveChangesAsync();
        }
    }
}
