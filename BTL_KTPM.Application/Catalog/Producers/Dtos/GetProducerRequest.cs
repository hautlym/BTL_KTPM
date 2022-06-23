using BTL_KTPM.Application.Catalog.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_KTPM.Application.Catalog.Producers.Dtos
{
    public class GetProducerRequest:PageResultBase
    {
        public string? keyword { get; set; }
    }
}
