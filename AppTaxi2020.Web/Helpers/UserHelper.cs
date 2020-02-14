using AppTaxi2020.Web.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTaxi2020.Web.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserHelper(UserManager<UserEntity> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public Task<IdentityResult> AddUserAsync(UserEntity userEntity, string password)
        {
            throw new NotImplementedException();
        }

        public async Task AddUserToRoleAsync(UserEntity userEntity, string roleName)
        {
            throw new NotImplementedException();
        }

        public async Task CheckRoleAsyn(string roleName)
        {
            throw new NotImplementedException();
        }

        public async Task<UserEntity> GetUserByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsUserInRoleAsync(UserEntity userEntity, string roleName)
        {
            throw new NotImplementedException();
        }
    }
}
