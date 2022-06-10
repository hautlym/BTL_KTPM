using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_KTPM.Application.Catalog.Users.Dtos
{
    public class CreateUserRequest
    {
        public string UserName { get; set; }
        public int Password { get; set; }
        public int Name { get; set; }
        public string Dob { get; set; }
        public string Address { get; set; }
        public bool Sex { get; set; }
    }
}
