using BTL_KTPM.Application.Catalog.Common;
using BTL_KTPM.Application.Catalog.Orders.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_KTPM.Application.Catalog.Orders
{
    public interface IManageOrder
    {
        Task<List<OrderViewModel>> getAllOrder();
        Task<List<OrderViewModel>> GetAllCartByUserId(Guid UserId);
        Task<PageResult<OrderViewModel>> GetAlllPaging(GetOrderRequest request);
        Task<int> Create(CreateOrderRequest request);
        Task<int> Delete(int OrderId);
        Task<int> DeleteCart(Guid UserId);
        Task<int> DeleteOrderTail(int OrderId);
        Task<OrderViewModel> GetById(int OrderId);
    }
}
