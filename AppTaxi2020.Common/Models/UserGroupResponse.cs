using System;
using System.Collections.Generic;
using System.Text;

namespace AppTaxi2020.Common.Models
{
    public class UserGroupResponse
    {
        public int Id { get; set; }

        public UserResponse User { get; set; }

        public List<UserGroupDetailResponse> Users { get; set; }

    }
}
