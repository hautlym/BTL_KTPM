using BTL_KTPM.Data.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_KTPM.Application.Catalog.Carts
{
    public class CartViewModel
    {
        public int id { get; set; }
        public string ProductNane { get; set; }
        public int Quantity { get; set; }
        public double ProductPrice { get; set; }
        public string UserName { get; set; }
        public double Discount { get; set; }
        public string? ImgUrl { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }

    }
}
