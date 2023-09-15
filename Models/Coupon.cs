using System.ComponentModel.DataAnnotations;

namespace ETicaret.Models
{
    public class Coupon
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int Percentage { get; set; }
    }
}
