using BTL_KTPM.Application.Catalog.Common;
using BTL_KTPM.Application.Catalog.System.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_KTPM.Application.Catalog.System
{
    public interface IUserService
    {
        Task<ApiResult<string>> Authencate(LoginRequest request);
        Task<ApiResult<bool>> Register(RegisterRequest request);
        Task<ApiResult<bool>> Update(Guid id ,UserUpdateRequest request);

        Task<ApiResult<PageResult<UserViewModels>>> GetUserPaging(GetUserPagingRequest request);
        Task<ApiResult<bool>> delete(Guid id);
        Task<ApiResult<UserViewModels>> GetById(Guid id);
        public Task<ApiResult<bool>> RoleAssign(Guid id, RolesAssignRequest request);

    }
}
