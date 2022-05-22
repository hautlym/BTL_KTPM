using BTL_KTPM.Application.Catalog.Common;
using BTL_KTPM.Application.Catalog.Products.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_KTPM.Application.Catalog.Products
{
    public interface IPublicProductService
    {
        public Task<PageResult<ProductViewModel>> getAllByCategoryId(GetProductPagingRequest request);
    }
}
