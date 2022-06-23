using BTL_KTPM.Application.Catalog.Categories;
using BTL_KTPM.Application.Catalog.Categories.Dtos;
using BTL_KTPM.Application.Catalog.Common;
using BTL_KTPM.Application.Catalog.System.Dtos;

namespace BTL_KTPM.Admin_App.Service.CategoryApiClient
{
    public interface ICategoriesApiClient
    {
        Task<ApiResult<List<CategoryViewModels>>> GetAll();
        Task<PageResult<CategoryViewModels>> GetAllPaging(GetCategoryRequest request);
        Task<ApiResult<bool>> CreateCategory(CreateCategoryRequest request);
        Task<bool> UpdateCategory(int id, UpdateCategoryRequest request);
        Task<CategoryViewModels> GetById(int id);
        Task<bool> Delete(int id);
    }
}
