using BTL_KTPM.Application.Catalog.Common;
using BTL_KTPM.Application.Catalog.Orders.Dtos;
using BTL_KTPM.Data.EF;
using BTL_KTPM.Data.entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_KTPM.Application.Catalog.Orders
{
    public class ManageOrder : IManageOrder
    {
        private readonly BTL_KTPMDbContext _context;
        public ManageOrder(BTL_KTPMDbContext context)
        {
            _context = context;
        }
        public async Task<int> Create(CreateOrderRequest request)
        {

            var ListCart = _context.appUsers.Where(x => x.Id == request.AppUserId).Select(x => x.Carts).FirstOrDefault();

            var ListOrder = new List<OrderDetail>();
            foreach (var cart in ListCart)
            {
                var OrderDetail = new OrderDetail()
                {
                    ProductId = cart.ProductId,
                    Quantity = cart.Quantity,
                    Products = cart.Product,
                };
                ListOrder.Add(OrderDetail);
            }

            var Orders = new Order()
            {
                ShipName = request.ShipName,
                ShipAddress = request.ShipAddress,
                ShipDescription = request.ShipDescription,
                ShipEmail = request.ShipEmail,
                ShipNumberPhone = request.ShipNumberPhone,
                OrderDate = DateTime.Now,
                OrderDetails = ListOrder,
                AppUserId = request.AppUserId,

            };
            var kq = await _context.Orders.AddAsync(Orders);
            var order = _context.Orders.ToList();
            await _context.SaveChangesAsync();
            return Orders.Id;
        }

        public async Task<int> Delete(int OrderId)
        {
            var order = _context.Orders.Where(x => x.Id == OrderId).FirstOrDefault();
            if (order == null) throw new BTL_KTPMException("Can not find cart");
                _context.Orders.Remove(order);
            
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteCart(Guid UserId)
        {
            foreach (var item in _context.Carts)
            {
                if (item.AppUserId == UserId)
                {
                    _context.Carts.Remove(item);
                }
            }
            return await _context.SaveChangesAsync();
        }

        public Task<int> DeleteOrderTail(int OrderId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<OrderViewModel>> GetAllCartByUserId(Guid UserId)
        {
            var order = _context.Orders;
            var data = await order.Where(x => x.AppUserId == UserId).Select(x => new OrderViewModel()
            {
                Id = x.Id,
                ShipName = x.ShipName,
                ShipAddress = x.ShipAddress,
                ShipDescription = x.ShipDescription,
                ShipEmail = x.ShipEmail,
                ShipNumberPhone = x.ShipNumberPhone,
                OrderDate = DateTime.Now,
                OrderDetails = x.OrderDetails
            }).ToListAsync();
            return data;
        }

        public async Task<PageResult<OrderViewModel>> GetAlllPaging(GetOrderRequest request)
        {
            var query = from c in _context.Orders
                        select c;
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.ShipName.Contains(request.Keyword) || x.ShipNumberPhone.Contains(request.Keyword));
            }
            var totalRow = query.Count();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).Select(x => new OrderViewModel()
            {
                Id = x.Id,
                ShipName = x.ShipName,
                ShipNumberPhone = x.ShipNumberPhone,
                ShipAddress = x.ShipAddress,
                ShipDescription = x.ShipDescription,
                ShipEmail = x.ShipEmail,
                OrderDate = x.OrderDate,
                OrderDetails = x.OrderDetails
                
            }).ToListAsync();
            var pageResult = new PageResult<OrderViewModel>
            {
                TotalRecords = totalRow,
                Items = data,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
            };
            return pageResult;
        }

        public Task<List<OrderViewModel>> getAllOrder()
        {
            throw new NotImplementedException();
        }

        public async Task<OrderViewModel> GetById(int OrderId)
        {
            var query =( from o in _context.Orders
                        select o).ToList();
            var data = await _context.Orders.Where(x => x.Id == OrderId).Select(x => new OrderViewModel()
            {
                Id = x.Id,
                ShipName = x.ShipName,
                ShipNumberPhone = x.ShipNumberPhone,
                ShipAddress = x.ShipAddress,
                ShipDescription = x.ShipDescription,
                ShipEmail = x.ShipEmail,
                OrderDate = x.OrderDate,
                OrderDetails = x.OrderDetails
            }).FirstOrDefaultAsync();
            if (data != null)
            {
                return data;
            }
            else
            {
                return null;
            }
        }

    }
}
