﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_KTPM.Data.entities
{
    public class User
    {
        public int id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Dob { get; set; }
        public string Address { get; set; }
        public string Roles { get; set; }
        public bool Sex { get; set; }
        public string? PhoneNumber { get; set; }
        //public List<Cart> Carts { get; set; }
    }
}
