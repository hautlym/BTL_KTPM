using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_KTPM.Application.Catalog.Producers.Dtos
{
    public class UpdateProducerRequest
    {
        public int id { get; set; }
        public string ProducerName { get; set; }
        public string Description { get; set; }
        public string SĐT { get; set; }
        public string Address { get; set; }
    }
}
