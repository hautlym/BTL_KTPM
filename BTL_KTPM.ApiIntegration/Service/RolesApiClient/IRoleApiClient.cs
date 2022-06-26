using BTL_KTPM.Application.Catalog.System.Dtos;

namespace BTL_KTPM.ApiIntegration.Service.RolesApiClient
{
    public interface IRoleApiClient
    {
        Task<ApiResult<List<RolesViewModel>>> GetAll();
    }
}
