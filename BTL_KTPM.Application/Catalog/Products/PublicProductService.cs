using BTL_KTPM.Application.Catalog.Common;
using BTL_KTPM.Application.Catalog.Products.Dtos;
using BTL_KTPM.Data.EF;
using BTL_KTPM.Data.entities;
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
            //var img = from pi in _context.productImgs
            //          group pi by pi.ProductId into ng
            //          select new  {Id = ng.Key, imagePath = ng.ToList()};
            var query = from product in _context.products
                        select product;
            //var imgdata = img.ToList();
            var data = await query
            .Select(x => new ProductViewModel()
             {
                 Id = x.Id,
                 ProductName = x.ProductName,
                 ProductPrice = x.ProductPrice,
                 ProductDescription = x.ProductDescription,
                 ProductOriginalPrice = x.ProductOriginalPrice,
                 ProductTitle = x.ProductTitle,
                 CountComment = x.CountComment,
                 ProducerId = x.ProducerId,
                 IsNewProduct = x.IsNewProduct,
                 Discount = x.Discount,
                 CategoryId = x.CategoryId,
                 ThumbnailImage = x.productImgs
            }).ToListAsync();
            
            return data;
        }

        public async Task<PageResult<ProductViewModel>> getAllByCategoryId(GetPublicProductRequest request)
        {
            var query = from product in _context.products
                        join c in _context.Categories on product.CategoryId equals c.Id
                        join pi in _context.productImgs on product.Id equals pi.ProductId
                        select new { product, c, pi };

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
                    CategoryId = x.product.CategoryId,
                    ThumbnailImage = x.product.productImgs
                }).ToListAsync();
            var pageResult = new PageResult<ProductViewModel>
            {
                TotalRecords = totalRow,
                Items = data
            };
            return pageResult;
        }

        public async Task<List<ProductViewModel>> GetProductByName(string Name)
        {
            var query = from product in _context.products
                        where product.ProductName.Contains(Name)
                        select product;
            //var imgdata = img.ToList();
            var data = await query
            .Select(x => new ProductViewModel()
            {
                Id = x.Id,
                ProductName = x.ProductName,
                ProductPrice = x.ProductPrice,
                ProductDescription = x.ProductDescription,
                ProductOriginalPrice = x.ProductOriginalPrice,
                ProductTitle = x.ProductTitle,
                CountComment = x.CountComment,
                ProducerId = x.ProducerId,
                IsNewProduct = x.IsNewProduct,
                Discount = x.Discount,
                CategoryId = x.CategoryId,
                ThumbnailImage = x.productImgs
            }).ToListAsync();

            return data;
        }
    }
}
