using BTL_KTPM.Application.Catalog.Carts.Dtos;
using BTL_KTPM.Application.Catalog.System.Dtos;
using BTL_KTPM.Data.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_KTPM.Application.Catalog.Carts
{
    public interface IManageCart
    {
        Task<List<CartViewModel>> GetAllCart();
        Task<List<CartViewModel>> GetAllCartByUserId(Guid UserId);
        Task<int> Create(CreateCartRequest request);
        Task<int> Update(UpdateCartRequest request);
        Task<int> Delete(int CategoryId);
        Task<CartViewModel> GetById(int categoryId);
    }
}
