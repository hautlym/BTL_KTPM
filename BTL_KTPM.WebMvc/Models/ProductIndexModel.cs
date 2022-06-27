using BTL_KTPM.Application.Catalog.Categories;
using BTL_KTPM.Application.Catalog.Products.Dtos;

namespace BTL_KTPM.WebMvc.Models
{
    public class ProductIndexModel
    {
        public List<ProductViewModel> ListProduct { get; set; }
        public List<CategoryViewModels> Category { get; set; }
        public List<ProductViewModel> TopProduct { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int TotalRecords { get; set; }
        public int PageCount { get; set; }
    }
}
