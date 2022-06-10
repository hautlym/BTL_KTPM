using BTL_KTPM.Application.Catalog.Carts.Dtos;
using BTL_KTPM.Application.Catalog.Common;
using BTL_KTPM.Data.EF;
using BTL_KTPM.Data.entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_KTPM.Application.Catalog.Carts
{
    public class ManagerCart : IManageCart
    {
        private readonly BTL_KTPMDbContext _context;

        public ManagerCart(BTL_KTPMDbContext context)
        {
            _context = context;
        }
        public async Task<int> Create(CreateCartRequest request)
        {
            var cart = new Cart()
            {
                ProductId = request.ProductId,
                Price = request.Price,
                UserId = request.UserId,
                Quantity = request.Quantity,
            };
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
            return cart.Id;
        }

        public async Task<int> Delete(int CartId)
        {
            var cart = _context.Carts.Where(x => x.Id == CartId).FirstOrDefault();
            if (cart == null) throw new BTL_KTPMException("Can not find cart");
            _context.Carts.Remove(cart);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<CartViewModel>> GetAllCart(int UserId)
        {
            var data = await _context.Carts.Where(x=>x.UserId==UserId).Select(x => new CartViewModel()
            {
                
            }).ToListAsync();
            return data;
        }

        public Task<CartViewModel> GetById(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(UpdateCartRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
