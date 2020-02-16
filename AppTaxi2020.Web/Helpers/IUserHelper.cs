using AppTaxi2020.Web.Data.Entities;
using AppTaxi2020.Web.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace AppTaxi2020.Web.Helpers
{
    public interface IUserHelper
    {
        Task<UserEntity> GetUserByEmailAsync(string email);

        Task<IdentityResult> AddUserAsync(UserEntity user, string password);

        Task CheckRoleAsync(string roleName);

        Task AddUserToRoleAsync(UserEntity user, string roleName);

        Task<bool> IsUserInRoleAsync(UserEntity user, string roleName);

        Task<SignInResult> LoginAsync(LoginViewModel loginViewModel);
        Task LogoutAsync();
    }
}
