using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_KTPM.Application.Catalog.Contacts.Dtos
{
    public class CreateContactRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string NumberPhone { get; set; }
        public string Message { get; set; }
    }
}
