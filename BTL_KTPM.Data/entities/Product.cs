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
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public string ProductTitle { get; set; }
        public double ProductOriginalPrice { get; set; }
        public string ProductDescription { get; set; }
        public  bool IsNewProduct { get; set; }
        public int CountComment { get; set; }
        public double Discount { get; set; }
        public int ProducerId { get; set; }
        public int CategoryId { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public List<Cart> Carts { get; set; }
        public Producer Producers { get; set; }
        public Category category { get; set; }
        public List<ProductImg> productImgs { get; set; }

    }
}
