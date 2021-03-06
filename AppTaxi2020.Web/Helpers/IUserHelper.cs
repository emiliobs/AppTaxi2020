﻿using AppTaxi2020.Web.Data.Entities;
using AppTaxi2020.Web.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace AppTaxi2020.Web.Helpers
{
    public interface IUserHelper
    {

        Task<string> GeneratePasswordResetTokenAsync(UserEntity user);
        Task<IdentityResult> ResetPasswordAsync(UserEntity user, string token, string password);
        Task<string> GenerateEmailConfirmationTokenAsync(UserEntity user);
        Task<IdentityResult> ConfirmEmailAsync(UserEntity user, string token);
        Task<UserEntity> GetUserAsync(string email);
        Task<UserEntity> GetUserAsync(Guid  userId);
        Task<IdentityResult> AddUserAsync(UserEntity user, string password);
        Task CheckRoleAsync(string roleName);
        Task AddUserToRoleAsync(UserEntity user, string roleName);
        Task<bool> IsUserInRoleAsync(UserEntity user, string roleName);
        Task<SignInResult> LoginAsync(LoginViewModel loginViewModel);
        Task LogoutAsync();
        Task<UserEntity> AddUserAsync(AddUserViewModel model, string path);
        Task<IdentityResult> ChangePasswordAsync(UserEntity user, string oldPassword, string newPassword);
        Task<IdentityResult> UpdateUserAsync(UserEntity user);
        Task<SignInResult> ValidatePasswordAsync(UserEntity user, string password);


    }
}
