using BTL_KTPM.Application.Catalog.Products.Dtos;

namespace BTL_KTPM.WebMvc.Models
{
    public class HomeViewModels
    {
        public List<ProductViewModel> NewsProduct { get; set; }
        public List<ProductViewModel> DiscountProduct { get; set; }
        public List<ProductViewModel> PopularProduct { get; set; }
        public List<ProductViewModel> ProductInShop { get; set; }
    }
}
