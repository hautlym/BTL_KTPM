using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_KTPM.Data.entities
{
    public class Producer
    {
        public int ProducerId { get; set; }
        public string ProducerName { get; set; }
        public string Description { get; set; }
        public List<Product> Products { get; set; }

         
    }
}
