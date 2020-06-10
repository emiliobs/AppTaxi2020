﻿using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;
using AppTaxi2020.Common.Models;
using AppTaxi2020.Web.Data;
using AppTaxi2020.Web.Data.Entities;
using AppTaxi2020.Web.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppTaxi2020.Web.Controllers.API
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly AppDataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly IConverterHelper _converterHelper;

        public TripsController(AppDataContext context, IUserHelper userHelper, IConverterHelper converterHelper)
        {
            this._context = context;
            this._userHelper = userHelper;
            this._converterHelper = converterHelper;
        }

        [HttpPost]
        public async Task<IActionResult> PostTripEntity([FromBody] TripRequest tripRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userEntity = await _userHelper.GetUserAsync(tripRequest.UserId);
            if (userEntity == null)
            {
                return BadRequest("User doesn't exists.");
            }

            var taxEntity = await _context.Taxis.FirstOrDefaultAsync(t => t.Plaque == tripRequest.Plaque);
            if (taxEntity == null)
            {
                _context.Taxis.Add(new TaxiEntity 
                { 
                   Plaque = tripRequest.Plaque.ToUpper(),
                   
                });

                await _context.SaveChangesAsync();
                taxEntity = await _context.Taxis.FirstOrDefaultAsync(t => t.Plaque == tripRequest.Plaque);
            }

            var tripEntity = new TripEntity
            {
                Source = tripRequest.Address,
                SourceLatitude = tripRequest.Latitude,
                SourceLongitude = tripRequest.Longitude,
                StartDate = DateTime.UtcNow,
                Taxi = taxEntity,
                TripDetails = new List<TripDetailEntity>
                { 
                    new TripDetailEntity
                    {
                       Date = DateTime.UtcNow,
                       Latitude = tripRequest.Latitude,
                       Longitude = tripRequest.Longitude
                    }
                },
                UserEntity = userEntity,
            
            };

            _context.Trips.Add(tripEntity);
            await _context.SaveChangesAsync();
            return Ok(_converterHelper.ToTripResponse(tripEntity));

        }

        [HttpPost]
        [Route("CompleteTrip")]
        public async Task<IActionResult> CompleteTrip([FromBody] CompleteTripRequest completeTripRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trip = await _context.Trips.Include(t => t.TripDetails).FirstOrDefaultAsync(t => t.Id == completeTripRequest.TripId);
            if (trip == null)
            {
                return BadRequest("Trip not found.");
            }

            trip.EndDate = DateTime.UtcNow;
            trip.Qualification = completeTripRequest.Qualification;
            trip.Remarks = completeTripRequest.Remarks;
            trip.Target = completeTripRequest.Target;
            trip.TargetLatitude = completeTripRequest.TargetLatitude;
            trip.TargetLongitude = completeTripRequest.TargetLongitude;
            trip.TripDetails.Add(new TripDetailEntity 
            { 
               Date = DateTime.UtcNow,
               Latitude = completeTripRequest.TargetLatitude,
               Longitude = completeTripRequest.TargetLongitude,
            });

            _context.Trips.Update(trip);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
