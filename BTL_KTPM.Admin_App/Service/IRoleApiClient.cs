using BTL_KTPM.Application.Catalog.System.Dtos;

namespace BTL_KTPM.Admin_App.Service
{
    public interface IRoleApiClient
    {
        Task<ApiResult<List<RolesViewModel>>> GetAll();
    }
}
