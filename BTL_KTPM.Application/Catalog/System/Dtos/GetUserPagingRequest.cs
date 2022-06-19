using BTL_KTPM.Application.Catalog.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_KTPM.Application.Catalog.System.Dtos
{
    public class GetUserPagingRequest:PagingRequestBase
    {
        public string? Keyword { get; set; }
    }
}
