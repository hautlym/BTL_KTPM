using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_KTPM.Data.entities
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductImg1 { get; set; }
        public string ProductImg2 { get; set; }
        public string ProductImg3 { get; set; }
        public string ProductImg4 { get; set; }
        public string ProductName { get; set; }
        public string ProductPrice { get; set; }
        public string ProductTitle { get; set; }
        public string ProductOriginalPrice { get; set; }
        public string ProductDescription { get; set; }
        public  bool IsNewProduct { get; set; }
        public int CountComment { get; set; }
        public double Discount { get; set; }
        public int ProducerId { get; set; }
        public List<OrderDetail> OrderDetail { get; set; }
        public List<ProductInCategory> ProductInCategory { get; set; }
        public List<Cart> Carts { get; set; }
        public Producer Producers { get; set; }

    }
}
