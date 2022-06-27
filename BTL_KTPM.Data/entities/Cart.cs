using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_KTPM.Data.entities
{
    public class Cart
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Guid AppUserId { get; set; }
        public Product Product { get; set; }
        public DateTime DateCreated { get; set; }
        public AppUser AppUser { get; set; }
    }
}
