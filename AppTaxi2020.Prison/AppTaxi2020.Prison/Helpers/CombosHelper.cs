using AppTaxi2020.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppTaxi2020.Prison.Helpers
{
    public class CombosHelper
    {
        public static List<Role> GetRoles()
        {
            return new List<Role>
            {
               new Role{ Id = 1, Name = Languages.User },
               new Role{ Id = 2, Name = Languages.Driver }
            };
        }
    }
}
