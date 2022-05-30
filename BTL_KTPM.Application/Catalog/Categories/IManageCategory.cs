using BTL_KTPM.Application.Catalog.Categories.Dtos;
using BTL_KTPM.Data.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_KTPM.Application.Catalog.Categories
{
    public interface IManageCategory
    {
        Task<List<CategoryViewModels>> GetAllCategory();
        Task<int> Create(CreateCategoryRequest request);
        Task<int> Update(UpdateCategoryRequest request);
        Task<int> Delete(int CategoryId);
        Task<CategoryViewModels> GetById(int categoryId);
    }
}
