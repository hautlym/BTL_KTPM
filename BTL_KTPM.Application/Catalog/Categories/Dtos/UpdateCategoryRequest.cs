using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_KTPM.Application.Catalog.Categories.Dtos
{
    public class UpdateCategoryRequest
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string  Description { get; set; }
    }
}
