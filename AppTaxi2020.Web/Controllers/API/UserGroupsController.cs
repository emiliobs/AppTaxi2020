using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppTaxi2020.Web.Data;
using AppTaxi2020.Web.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppTaxi2020.Web.Controllers.API
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class UserGroupsController : ControllerBase
    {
        private readonly AppDataContext _context;
        private readonly IConverterHelper _converterHelper;

        public UserGroupsController(AppDataContext context, IConverterHelper converterHelper)
        {
            this._context = context;
            this._converterHelper = converterHelper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserGroup([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userGroup = await _context.UserGroupEntities.Include(ug => ug.Users).ThenInclude(u => u.User)
                                 .FirstOrDefaultAsync(u => u.User.Id == id);

            if (userGroup == null || userGroup?.Users == null)
            {
                return Ok();
            }

            return Ok(_converterHelper.ToUserGroupResponse(userGroup.Users.ToList()));
        }
    }
}
