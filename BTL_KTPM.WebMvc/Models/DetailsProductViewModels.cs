using BTL_KTPM.Application.Catalog.Products.Dtos;
using BTL_KTPM.Data.entities;

namespace BTL_KTPM.WebMvc.Models
{
    public class DetailsProductViewModels
    {
        public ProductViewModel ProductDeltails { get; set; }
        public List<ProductViewModel> SuggestProduct { get; set; }
    }
}
