using BTL_KTPM.Application.Catalog.Common;
using BTL_KTPM.Application.Catalog.Products.Dtos;
using BTL_KTPM.Application.Catalog.System.Dtos;

namespace BTL_KTPM.ApiIntegration.Service.ProductApiClient
{
    public interface IProductApiClient
    {
        Task<ApiResult<PageResult<ProductViewModel>>> GetPaging(GetProductPagingRequest request);
        Task<List<ProductViewModel>> GetAll();
        Task<bool> CreateProduct(ProductCreateRequest request);
        Task<bool> UpdateProduct(int id, ProductUpdateRequest request);
        Task<ProductViewModel> GetById(int id);
        Task<bool> Delete(int id);
    }
}
