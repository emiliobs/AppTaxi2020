using AppTaxi2020.Common.Enums;
using AppTaxi2020.Web.Data.Entities;
using AppTaxi2020.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTaxi2020.Web.Data
{
    public class SeedDb
    {
        private readonly AppDataContext _dataContext;
        private readonly IUserHelper _userHelper;

        public SeedDb(
            AppDataContext dataContext,
            IUserHelper userHelper)
        {
            _dataContext = dataContext;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _dataContext.Database.EnsureCreatedAsync();
            await CheckRolesAsync();
            await CheckUserAsync("1010", "Emilio", "Barrera", "barrera_emilio@hotmail.com", "4407907951284", "London", UserType.Admin);
            UserEntity driver = await CheckUserAsync("2020", "Emilio", "Barrera", "emiliobarrerasepulveda@gmail.com", "4407907951284", "London", UserType.Driver);
            UserEntity user1 = await CheckUserAsync("3030", "Emilio", "Barrera", "barrera_emilio@yahoo.es", "4407907951284", "London", UserType.User);
            UserEntity user2 = await CheckUserAsync("4040", "Emilio", "Barrera", "emilio@gmail.com", "4407907951284", "London", UserType.User);
            await CheckTaxisAsync(driver, user1, user2);
        }

        private async Task<UserEntity> CheckUserAsync(
            string document,
            string firstName,
            string lastName,
            string email,
            string phone,
            string address,
            UserType userType)
        {
            AppTaxi2020.Web.Data.Entities.UserEntity user = await _userHelper.GetUserByEmailAsync(email);
            if (user == null)
            {
                user = new UserEntity
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document,
                    UserType = userType
                };

                await _userHelper.AddUserAsync(user, "Eabs123.");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());
            }

            return user;
        }

        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.Driver.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
        }

        private async Task CheckTaxisAsync(
            UserEntity driver,
            UserEntity user1,
            UserEntity user2)
        {
            if (!_dataContext.Taxis.Any())
            {
                _dataContext.Taxis.Add(new TaxiEntity
                {
                    UserEntity = driver,
                    Plaque = "TPQ123",
                    Trips = new List<TripEntity>
                    {
                        new TripEntity
                        {
                            StartDate = DateTime.UtcNow,
                            EndDate = DateTime.UtcNow.AddMinutes(30),
                            Qualification = 4.5f,
                            Source = "ITM Fraternidad",
                            Target = "ITM Robledo",
                            Remarks = "Phasellus velit dui, pretium rutrum interdum sed, vehicula ut arcu. Nulla interdum consectetur fermentum. Donec ac libero lectus. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Aenean sagittis enim sed laoreet pharetra. Suspendisse imperdiet maximus consectetur. Pellentesque blandit justo odio, a tristique turpis semper eu. Nullam felis massa, varius vitae mi placerat, vulputate scelerisque enim. Nam auctor eros in velit gravida, et pulvinar ante suscipit. Proin at ligula odio. Integer luctus vel augue sed hendrerit. Maecenas sed ligula interdum, pulvinar turpis ut, bibendum nibh. Sed vitae ipsum magna. Suspendisse ultrices nisl quis metus malesuada blandit. Mauris turpis augue, vestibulum a auctor id, aliquam ac diam.",
                            UserEntity = user1
                        },
                        new TripEntity
                        {
                            StartDate = DateTime.UtcNow,
                            EndDate = DateTime.UtcNow.AddMinutes(30),
                            Qualification = 4.8f,
                            Source = "ITM Robledo",
                            Target = "ITM Fraternidad",
                            Remarks = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin sit amet convallis mauris, id gravida leo. Phasellus lorem felis, porta vel risus tristique, sodales feugiat erat. Morbi commodo, ex in congue placerat, justo augue egestas nisl, at lacinia quam tellus a sem. Nullam scelerisque aliquam ligula at semper. Nam blandit orci et rutrum consectetur. Aenean non faucibus ligula. In non purus iaculis, congue urna et, pretium neque. Nunc sed viverra orci. Mauris finibus dolor a tristique mattis.",
                            UserEntity = user1
                        }
                    }
                });

                _dataContext.Taxis.Add(new TaxiEntity
                {
                    Plaque = "THW321",
                    UserEntity = driver,
                    Trips = new List<TripEntity>
                    {
                        new TripEntity
                        {
                            StartDate = DateTime.UtcNow,
                            EndDate = DateTime.UtcNow.AddMinutes(30),
                            Qualification = 4.5f,
                            Source = "ITM Fraternidad",
                            Target = "ITM Robledo",
                            Remarks = "Muy buen servicio",
                            UserEntity = user2
                        },
                        new TripEntity
                        {
                            StartDate = DateTime.UtcNow,
                            EndDate = DateTime.UtcNow.AddMinutes(30),
                            Qualification = 4.8f,
                            Source = "ITM Robledo",
                            Target = "ITM Fraternidad",
                            Remarks = "Conductor muy amable",
                            UserEntity = user2
                        }
                    }
                });

                await _dataContext.SaveChangesAsync();
            }
        }
    }
}