using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppTaxi2020.Web.Data;
using AppTaxi2020.Web.Data.Entities;
using AppTaxi2020.Web.Helpers;

namespace AppTaxi2020.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxisController : ControllerBase
    {
        private readonly AppDataContext _context;
        private readonly IConverterHelper _converterHelper;

        public TaxisController(AppDataContext context, IConverterHelper converterHelper)
        {
            _context = context;
            _converterHelper = converterHelper;
        }


        // GET: api/Taxis/5
        [HttpGet("{plaque}")]
        public async Task<ActionResult<TaxiEntity>> GetTaxiEntity(string plaque)
        {
            var taxiEntity = await _context.Taxis
                .Include(t => t.UserEntity)//Driver
                .Include(t => t.Trips)
                .ThenInclude(t => t.TripDetails)
                .Include(t => t.Trips)
                .ThenInclude(t => t.UserEntity)    //Passenger
                .FirstOrDefaultAsync(t => t.Plaque == plaque);

            if (taxiEntity == null)
            {
                taxiEntity = new TaxiEntity 
                {
                   Plaque = plaque.ToUpper(),
                };
                _context.Taxis.Add(taxiEntity);
                await _context.SaveChangesAsync();


            }

            return Ok(_converterHelper.ToTaxiResponse(taxiEntity));
        }

       
    }
}
