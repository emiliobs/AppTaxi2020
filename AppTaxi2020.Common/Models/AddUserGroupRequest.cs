using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AppTaxi2020.Common.Models
{
    public class AddUserGroupRequest
    {
        [Required]
        public Guid UserId { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        public string CultureInfo { get; set; }

    }
}
