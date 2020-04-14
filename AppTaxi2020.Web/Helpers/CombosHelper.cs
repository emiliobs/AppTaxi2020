using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTaxi2020.Web.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        public IEnumerable<SelectListItem> GetComboRoles()
        {
            List<SelectListItem> list = new List<SelectListItem> 
            {
               new SelectListItem { Value = "0", Text = "[Select a Role.....]" },
               new SelectListItem{ Value = "1", Text = "Driver" },
               new SelectListItem{ Value = "2", Text="User" },
            };

            return list;
        }
    }
}
