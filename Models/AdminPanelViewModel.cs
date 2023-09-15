using ETicaret.Repositories;

namespace ETicaret.Models
{
    public class AdminPanelViewModel
    {
        public Product Product { get; set; }
        public Category Category { get; set; }
        public Subcategory Subcategory { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Subcategory> Subcategories { get; set; }
        public IEnumerable<Coupon> Coupons { get; set; }
        public IEnumerable<Shipper> Shippers { get; set; }
        public Dictionary<int, List<int>> CategorySubcategories { get; set; }
    }
}
