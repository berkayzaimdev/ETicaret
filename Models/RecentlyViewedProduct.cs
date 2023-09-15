using System.ComponentModel.DataAnnotations;

namespace ETicaret.Models
{
    public class RecentlyViewedProduct
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
    }
}
