using ETicaret.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int? CartId { get; set;  }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        [ForeignKey("CartId")]
        public Cart? Cart { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
