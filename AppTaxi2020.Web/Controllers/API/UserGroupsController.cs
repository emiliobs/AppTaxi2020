using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AppTaxi2020.Common.Enums;
using AppTaxi2020.Common.Models;
using AppTaxi2020.Web.Data;
using AppTaxi2020.Web.Data.Entities;
using AppTaxi2020.Web.Helpers;
using AppTaxi2020.Web.Resources;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppTaxi2020.Web.Controllers.API
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class UserGroupsController : ControllerBase
    {
        private readonly AppDataContext _context;
        private readonly IConverterHelper _converterHelper;
        private readonly IUserHelper _userHelper;
        private readonly IMailHelper _mailHelper;

        public UserGroupsController(
            AppDataContext context,
            IConverterHelper converterHelper,
            IUserHelper userHelper,
            IMailHelper mailHelper)
        {
            _context = context;
            _converterHelper = converterHelper;
            _userHelper = userHelper;
            _mailHelper = mailHelper;
        }

        [HttpPost]
        public async Task<IActionResult> PostUserGroup([FromBody] AddUserGroupRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CultureInfo cultureInfo = new CultureInfo(request.CultureInfo);
            Resource.Culture = cultureInfo;

            UserEntity proposalUser = await _userHelper.GetUserAsync(request.UserId);
            if (proposalUser == null)
            {
                return BadRequest(Resource.UserNotFoundError);
            }

            UserEntity requiredUser = await _userHelper.GetUserAsync(request.Email);
            if (requiredUser == null)
            {
                return BadRequest(Resource.UserNotFoundError);
            }

            UserGroupEntity userGroup = await _context.UserGroupEntities
                .Include(ug => ug.Users)
                .ThenInclude(u => u.User)
                .FirstOrDefaultAsync(ug => ug.User.Id == request.UserId.ToString());
            if (userGroup != null)
            {
                UserGroupDetailEntity user = userGroup.Users.FirstOrDefault(u => u.User.Email == request.Email);
                if (user != null)
                {
                    return BadRequest(Resource.UserAlreadyBelogToGroup);
                }
            }

            UserGroupRequestEntity userGroupRequest = new UserGroupRequestEntity
            {
                ProposalUser = proposalUser,
                RequiredUser = requiredUser,
                Status = UserGroupStatus.Pending,
                Token = Guid.NewGuid()
            };

            try
            {
                _context.UserGroupRequestEntities.Add(userGroupRequest);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            string linkConfirm = Url.Action("ConfirmUserGroup", "Account", new
            {
                requestId = userGroupRequest.Id,
                token = userGroupRequest.Token
            }, protocol: HttpContext.Request.Scheme);

            string linkReject = Url.Action("RejectUserGroup", "Account", new
            {
                requestId = userGroupRequest.Id,
                token = userGroupRequest.Token
            }, protocol: HttpContext.Request.Scheme);

            Response response = _mailHelper.SendMail(request.Email, Resource.RequestJoinGroupSubject, $"<h1>{Resource.RequestJoinGroupSubject}</h1>" +
                $"{Resource.TheUser}: {proposalUser.FullName} ({proposalUser.Email}), {Resource.RequestJoinGroupBody}" +
                $"</hr></br></br>{Resource.WishToAccept} <a href = \"{linkConfirm}\">{Resource.Confirm}</a>" +
                $"</hr></br></br>{Resource.WishToReject} <a href = \"{linkReject}\">{Resource.Reject}</a>");

            if (!response.IsSuccess)
            {
                return BadRequest(response.Message);
            }

            return Ok(Resource.RequestJoinGroupEmailSent);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserGroup([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserGroupEntity userGroup = await _context.UserGroupEntities
                .Include(ug => ug.Users)
                .ThenInclude(u => u.User)
                .FirstOrDefaultAsync(u => u.User.Id == id);

            if (userGroup == null || userGroup?.Users == null)
            {
                return Ok();
            }

            return Ok(_converterHelper.ToUserGroupResponse(userGroup.Users.ToList()));
        }
    }
}
