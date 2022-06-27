using BTL_KTPM.Application.Catalog.Orders.Dtos;
using BTL_KTPM.Data.entities;

namespace BTL_KTPM.Admin_App.Models
{
    public class ModelViewOrderDetail
    {
        public OrderViewModel OrderModel { get; set; }
        public List<Product> product { get; set; }

    }
}
