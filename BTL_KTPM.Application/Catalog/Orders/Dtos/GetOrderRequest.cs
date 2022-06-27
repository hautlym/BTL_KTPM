using BTL_KTPM.Application.Catalog.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_KTPM.Application.Catalog.Orders.Dtos
{
    public class GetOrderRequest: PagingRequestBase
    {
        public string? Keyword { get; set; }
    }
}
