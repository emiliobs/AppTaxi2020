﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTaxi2020.Web.Data.Entities
{
    public class UserGroupDetailEntity
    {
        public int Id { get; set; }

        public UserEntity User { get; set; }

        public UserGroupEntity UserGroup { get; set; }

    }
}
