﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_KTPM.Application.Catalog.Categories
{
    public class GetCategoryRequest
    {
        public int id { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }
}
