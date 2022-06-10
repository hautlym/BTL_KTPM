using BTL_KTPM.Application.Catalog.Carts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_KTPM.Application.Catalog.Carts
{
    public interface IManageCart
    {
        Task<List<CartViewModel>> GetAllCart(int userId);
        Task<int> Create(CreateCartRequest request);
        Task<int> Update(UpdateCartRequest request);
        Task<int> Delete(int CategoryId);
        Task<CartViewModel> GetById(int categoryId);
    }
}
