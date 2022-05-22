using BTL_KTPM.Application.Catalog.Common;
using BTL_KTPM.Application.Catalog.Products;
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
    public class ManageProductService : IManageProductService
    {
        private readonly BTL_KTPMDbContext _context;
        ManageProductService(BTL_KTPMDbContext context)
        {
            _context = context;
        }

        public async Task<int> Create(ProductCreateRequest request)
        {
            var product = new Product()
            {
                ProductName = request.ProductName,
                ProductDescription = request.ProductDescription,
                ProductOriginalPrice = request.ProductOriginalPrice,
                ProductTitle = request.ProductTitle,
                CountComment = 0,
                ProducerId = request.ProducerId,
                IsNewProduct = true,
                Discount = request.Discount,


            };
            _context.products.Add(product);
            return await _context.SaveChangesAsync();

        }

        public async Task<int> Delete(int productId)
        {
            var product = await _context.products.FindAsync(productId);
            if (product == null) throw new Exception("Can not find product");
            _context.products.Remove(product);
            return  await _context.SaveChangesAsync();
        }

        public List<ProductViewModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<PageResult<ProductViewModel>> GetAllPaging(GetProductPagingRequest request)
        {
            var query = from product in _context.products
                        join pic in _context.ProductInCategories on product.Id equals pic.ProductId
                        join c in _context.Categories on pic.CategoryId equals c.Id
                        select new { product,c };

            if(!string.IsNullOrEmpty(request.keyword))
            {
                query = query.Where(x => x.product.ProductName.Contains(request.keyword));
            }    
            if(request.CategoryId > 0)
            {
                query = query.Where(x=>x.c.Id == request.CategoryId);
            }
            int totalRow = await query.CountAsync();
            var data =await query.Skip((request.PageIndex - 1) * request.PageIndex).Take(request.PageSize)
                .Select(x=> new ProductViewModel()
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
                }).ToListAsync();
            var pageResult = new PageResult<ProductViewModel>
            {
                totalRecord = totalRow,
                Items = data
            };
            return pageResult;
        }

        public async Task<int> Update(ProductUpdateRequest request)
        {

            var product = await _context.products.FindAsync(request.Id);
            if (product == null) throw new Exception("Can not find product");
            product.ProductName = request.ProductName;
            product.ProductDescription = request.ProductDescription;
            product.ProductOriginalPrice = request.ProductOriginalPrice;
            product.ProductTitle = request.ProductTitle;
            product.CountComment = request.CountComment;
            product.ProducerId = request.ProducerId;
            product.IsNewProduct = request.IsNewProduct;
            product.Discount = request.Discount;
            return await _context.SaveChangesAsync();
        }
    }
}
