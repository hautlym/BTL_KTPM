using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_KTPM.Application.Catalog.Users
{
    public class UserViewModel
    {
        public int id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Dob { get; set; }
        public string Address { get; set; }
        public bool Sex { get; set; }
        public string Roles { get; set; }
        public string PhoneNumber { get; set; }
    }
}
