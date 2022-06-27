using BTL_KTPM.Application.Catalog.Common;
using BTL_KTPM.Application.Catalog.Orders.Dtos;
using BTL_KTPM.Application.Catalog.System.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_KTPM.ApiIntegration.Service.OrderApaiClient
{
    public interface IOrderApiClient
    {
        Task<List<OrderViewModel>> GetAll();
        Task<PageResult<OrderViewModel>> GetAllPaging(GetOrderRequest request);
        Task<List<OrderViewModel>> GetAllByUserId(Guid UserId);
        Task<ApiResult<bool>> CreateOrder(CreateOrderRequest request);
        Task<OrderViewModel> GetById(int id);
        Task<bool> Delete(int id);
        Task<bool> DeleteCart(Guid OrderId);
        Task<bool> DeleteOrderDetail(int OrderId);
    }
}
