﻿using BTL_KTPM.Application.Catalog.Common;
using BTL_KTPM.Application.Catalog.Products;
using BTL_KTPM.Application.Catalog.Products.Dtos;
using BTL_KTPM.Application.Catalog.Products.ProductImgs;
using BTL_KTPM.Application.Catalog.System.Dtos;
using BTL_KTPM.Data.EF;
using BTL_KTPM.Data.entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BTL_KTPM.Application.Catalog.Products
{
    public class ManageProductService : IManageProductService
    {
        private readonly BTL_KTPMDbContext _context;
        private readonly IStorageService _storageService;
        public ManageProductService(BTL_KTPMDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        public async Task<int> Create(ProductCreateRequest request)
        {

            var product = new Product()
            {
                ProductName = request.ProductName,
                ProductDescription = request.ProductDescription,
                ProductOriginalPrice = request.ProductOriginalPrice,
                ProductPrice = request.ProductOriginalPrice- (request.ProductOriginalPrice * (request.Discount)/100),
                ProductTitle = request.ProductTitle,
                CountComment = 0,
                ProducerId = request.ProducerId,
                IsNewProduct = true,
                Discount = request.Discount,
                CategoryId = request.CategoryId,

            };
            if (request.ThumbnailImage != null)
            {
                product.productImgs = new List<ProductImg>()
                {
                    new ProductImg()
                    {
                        Caption = "Thumbnail image",
                        DateCreated = DateTime.Now,
                        FileSize = request.ThumbnailImage.Length,
                        ImagePath = await this.SaveFile(request.ThumbnailImage),
                        IsDefault = true,
                        SortOrder = 1
                    }
                };
            }

            _context.products.Add(product);
             await _context.SaveChangesAsync();
            return product.Id;

        }

        public async Task<int> Delete(int productId)
        {
            var product = await _context.products.Where(x=>x.Id==productId).FirstOrDefaultAsync();
            if (product == null) throw new BTL_KTPMException("Can not find product");
            var image =  _context.productImgs.Where( x=>x.ProductId == productId);
            foreach (var img in image)
            {
                await _storageService.DeleteFileAsync(img.ImagePath);
            }    
            _context.products.Remove(product);
            return await _context.SaveChangesAsync();
        }

        public List<ProductViewModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult<PageResult<ProductViewModel>>> GetAllPaging(GetProductPagingRequest request)
        {
            var query = from product in _context.products
                        join c in _context.Categories on product.CategoryId equals c.Id
                        join p in _context.Producers on product.ProducerId equals p.ProducerId
                        select new { product, c,p };
            if (!string.IsNullOrEmpty(request.keyword))
            {
                query = query.Where(x => x.product.ProductName.Contains(request.keyword));
            }
            if(request.CategoryId != null && request.CategoryId !=0)
            {
                query = query.Where(x => x.c.Id == request.CategoryId);
            }
            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize)
                .Select(x => new ProductViewModel()
                {
                    Id = x.product.Id,
                    ProductName = x.product.ProductName,
                    ProductDescription = x.product.ProductDescription,
                    ProductOriginalPrice = x.product.ProductOriginalPrice,
                    ProductPrice = x.product.ProductPrice,
                    ProductTitle = x.product.ProductTitle,
                    CountComment = x.product.CountComment,
                    ProducerId = x.product.ProducerId,
                    ProducerName = x.p.ProducerName,
                    CategoryName = x.c.CategoryName,
                    IsNewProduct = x.product.IsNewProduct,
                    Discount = x.product.Discount,
                    CategoryId = x.product.CategoryId,
                    ThumbnailImage = x.product.productImgs
                    
                }).ToListAsync();
            var pageResult = new PageResult<ProductViewModel>
            {
                TotalRecords = totalRow,
                Items = data,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
            };
            return new ApiSuccessResult<PageResult<ProductViewModel>>(pageResult);
        }

        public async Task<int> Update(ProductUpdateRequest request)
        {
            
            var product = await _context.products.FindAsync(request.Id);
            if (product == null) throw new BTL_KTPMException("Can not find product");
            product.ProductName = request.ProductName;
            product.ProductPrice = request.ProductOriginalPrice - (request.ProductOriginalPrice * ((request.Discount) / 100));
            product.ProductDescription = request.ProductDescription;
            product.ProductOriginalPrice = request.ProductOriginalPrice;
            product.ProductTitle = request.ProductTitle;
            product.CountComment = request.CountComment;
            product.ProducerId = request.ProducerId;
            product.IsNewProduct = request.IsNewProduct;
            product.Discount = request.Discount;
            product.CategoryId = request.CategoryId;
            if (request.ThumbnailImage != null)
            {
                var thumbnailImage =await _context.productImgs.FirstOrDefaultAsync(x=>x.ProductId == request.Id);
                if(thumbnailImage != null)
                {
                    thumbnailImage.FileSize = request.ThumbnailImage.Length;
                    thumbnailImage.ImagePath = await this.SaveFile(request.ThumbnailImage);
                     _context.productImgs.Update(thumbnailImage);
                }
                
            }
            _context.Update(product);
            return await _context.SaveChangesAsync();
        }
        public async Task<string> SaveFile(IFormFile file)
        {

            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }

        public async Task<ProductViewModel> GetById(int productId)
        {
            var products = _context.products.Where(x=>x.Id == productId).Select(product=> new ProductViewModel()
            {
                Id = product.Id,
                ProductName = product.ProductName,
                ProductPrice = product.ProductPrice,
                ProductDescription = product.ProductDescription,
                ProductOriginalPrice = product.ProductOriginalPrice,
                ProductTitle = product.ProductTitle,
                CountComment = product.CountComment,
                ProducerId = product.ProducerId,
                IsNewProduct = product.IsNewProduct,
                Discount = product.Discount,
                CategoryId = product.CategoryId,
                ThumbnailImage = product.productImgs
            }).FirstOrDefault();
            //if(product == null) 
            //    throw new BTL_KTPMException("Can not find product");
            return products;
            
        }

        public async Task<int> AddImage(int productId, ProductImageCreateRequest request)
        {
            var productImage = new ProductImg()
            {
                Caption = request.Caption,
                DateCreated = DateTime.Now,
                IsDefault = request.IsDefault,
                ProductId = productId,
                SortOrder = request.SortOrder
            };

            if (request.ImageFile != null)
            {
                productImage.ImagePath = await this.SaveFile(request.ImageFile);
                productImage.FileSize = request.ImageFile.Length;
            }
            _context.productImgs.Add(productImage);
            await _context.SaveChangesAsync();
            return productImage.Id;
        }

        public async Task<int> RemoveImage(int imageId)
        {
            var productImage = await _context.productImgs.FindAsync(imageId);
            if (productImage == null)
                throw new BTL_KTPMException($"Cannot find an image with id {imageId}");
            _context.productImgs.Remove(productImage);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateImage(int imageId, ProductImageUpdateRequest request)
        {
            var productImage = await _context.productImgs.FindAsync(imageId);
            if (productImage == null)
                throw new BTL_KTPMException($"Cannot find an image with id {imageId}");

            if (request.ImageFile != null)
            {
                productImage.ImagePath = await this.SaveFile(request.ImageFile);
                productImage.FileSize = request.ImageFile.Length;
            }
            _context.productImgs.Update(productImage);
            return await _context.SaveChangesAsync();
        }

        public async Task<ProductImageViewModel> GetImageById(int imageId)
        {
            var image = await _context.productImgs.FindAsync(imageId);
            if (image == null)
                throw new BTL_KTPMException($"Cannot find an image with id {imageId}");

            var viewModel = new ProductImageViewModel()
            {
                Caption = image.Caption,
                DateCreated = image.DateCreated,
                FileSize = image.FileSize,
                Id = image.Id,
                ImagePath = image.ImagePath,
                IsDefault = image.IsDefault,
                ProductId = image.ProductId,
                SortOrder = image.SortOrder
            };
            return viewModel;
        }

        public async Task<List<ProductImageViewModel>> GetListImages(int productId)
        {
            return await _context.productImgs.Where(x => x.ProductId == productId)
                .Select(i => new ProductImageViewModel()
                {
                    Caption = i.Caption,
                    DateCreated = i.DateCreated,
                    FileSize = i.FileSize,
                    Id = i.Id,
                    ImagePath = i.ImagePath,
                    IsDefault = i.IsDefault,
                    ProductId = i.ProductId,
                    SortOrder = i.SortOrder
                }).ToListAsync();
        }
    }

}
