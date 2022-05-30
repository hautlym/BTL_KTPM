using BTL_KTPM.Application.Catalog.Categories.Dtos;
using BTL_KTPM.Application.Catalog.Common;
using BTL_KTPM.Data.EF;
using BTL_KTPM.Data.entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_KTPM.Application.Catalog.Categories
{
    public class ManageCategory : IManageCategory
    {
        private readonly BTL_KTPMDbContext _context;
        public ManageCategory(BTL_KTPMDbContext context)
        {
               _context = context;
        }
        public async Task<int> Create(CreateCategoryRequest request)
        {
            var category = new Category()
            {
                CategoryName = request.CategoryName,
                Description = request.Description
            };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category.Id;
        }

        public async Task<int> Delete(int CategoryId)
        {
            var categories = await _context.Categories.Where(x => x.Id == CategoryId).FirstOrDefaultAsync();
            if (categories == null) throw new BTL_KTPMException("Can not find product");
            _context.Categories.Remove(categories);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<CategoryViewModels>> GetAllCategory()
        {
            var data =await _context.Categories.Select(x=> new CategoryViewModels()
            {
                Id = x.Id,
                CategoryName = x.CategoryName,
                Description= x.Description
            }).ToListAsync();
            return data;

        }

        public async Task<CategoryViewModels> GetById(int categoryId)
        {
            var categories =await _context.Categories.Where(x => x.Id == categoryId).FirstOrDefaultAsync();

            if (categories != null)
            {
                var categoryViewModels = new CategoryViewModels()
                {
                    Id = categories.Id,
                    CategoryName = categories.CategoryName,
                    Description = categories.Description
                };
                return categoryViewModels;
            }
            else
            {
                return null;
            }
        }

        public async Task<int> Update(UpdateCategoryRequest request)
        {
            var category = await _context.Categories.Where(x=>x.Id==request.Id).FirstOrDefaultAsync();
            if (category == null) throw new BTL_KTPMException("Can not find product");
            category.CategoryName = request.CategoryName;
            category.Description = request.Description;
            _context.Categories.Update(category);
            return await _context.SaveChangesAsync();

        }
    }
}
