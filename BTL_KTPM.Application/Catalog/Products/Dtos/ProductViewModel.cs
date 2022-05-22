using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_KTPM.Application.Catalog.Products.Dtos
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductPrice { get; set; }
        public string ProductTitle { get; set; }
        public string ProductOriginalPrice { get; set; }
        public string ProductDescription { get; set; }
        public bool IsNewProduct { get; set; }
        public int CountComment { get; set; }
        public double Discount { get; set; }
        public int ProducerId { get; set; }
    }
}
