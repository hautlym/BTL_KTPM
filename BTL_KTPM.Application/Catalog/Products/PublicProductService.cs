using BTL_KTPM.Application.Catalog.Common;
using BTL_KTPM.Application.Catalog.Products.Dtos;
using BTL_KTPM.Data.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_KTPM.Application.Catalog.Products
{

    public class PublicProductService : IPublicProductService
    {
        private readonly BTL_KTPMDbContext _context;
        public PublicProductService(BTL_KTPMDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductViewModel>> GetAll()
        {
            //var query = from product in _context.products
            //            join pic in _context.ProductInCategories on product.Id equals pic.ProductId
            //            join c in _context.Categories on pic.CategoryId equals c.Id
            //            select new { product, c };
            var query = from product in _context.products
                        select new{ product};
            var data = await query
                .Select(x => new ProductViewModel()
                {
                    Id = x.product.Id,
                    ProductName = x.product.ProductName,
                    ProductPrice = x.product.ProductPrice,
                    ProductDescription = x.product.ProductDescription,
                    ProductOriginalPrice = x.product.ProductOriginalPrice,
                    ProductTitle = x.product.ProductTitle,
                    CountComment = x.product.CountComment,
                    ProducerId = x.product.ProducerId,
                    IsNewProduct = x.product.IsNewProduct,
                    Discount = x.product.Discount,
                    CategoryId = x.product.CategoryId,  
                }).ToListAsync();
            return data;
        }

        public async Task<PageResult<ProductViewModel>> getAllByCategoryId(GetPublicProductRequest request)
        {
            var query = from product in _context.products
                        join c in _context.Categories on product.CategoryId equals c.Id
                        select new { product, c };

            if (request.CategoryId > 0)
            {
                query = query.Where(x => x.c.Id == request.CategoryId);
            }
            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageIndex).Take(request.PageSize)
                .Select(x => new ProductViewModel()
                {
                    Id = x.product.Id,
                    ProductName = x.product.ProductName,
                    ProductDescription = x.product.ProductDescription,
                    ProductOriginalPrice = x.product.ProductOriginalPrice,
                    ProductTitle = x.product.ProductTitle,
                    CountComment = x.product.CountComment,
                    ProducerId = x.product.ProducerId,
                    IsNewProduct = x.product.IsNewProduct,
                    Discount = x.product.Discount,
                    CategoryId = x.product.CategoryId
                }).ToListAsync();
            var pageResult = new PageResult<ProductViewModel>
            {
                totalRecord = totalRow,
                Items = data
            };
            return pageResult;
        }
    }
}
