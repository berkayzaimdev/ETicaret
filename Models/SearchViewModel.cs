namespace ETicaret.Models
{
    public class SearchViewModel
    {
        public List<Product> Products { get; set; }
        public List<Wishlist>? Wishlists { get; set; }
        public int CategoryId { get; set; }
        public int? SubcategoryId { get; set; }
        public string MenuName { get; set; } 
        public string? SearchTerm { get; set; }
        public List<Product> RecentlyViewedProducts { get; set; } 
        public List<Category> Categories { get; set; }
    }
}
