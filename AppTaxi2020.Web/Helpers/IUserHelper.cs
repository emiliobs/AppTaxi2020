using AppTaxi2020.Web.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTaxi2020.Web.Helpers
{
    public interface IUserHelper
    {
        Task<UserEntity> GetUserByEmailAsync(string email);
        Task<IdentityResult> AddUserAsync(UserEntity userEntity, string password);
        Task CheckRoleAsyn(string roleName);
        Task AddUserToRoleAsync(UserEntity userEntity, string roleName);
        Task<bool> IsUserInRoleAsync(UserEntity userEntity, string roleName);
    }
}
