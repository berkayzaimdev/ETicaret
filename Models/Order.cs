using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETicaret.Models
{
    public class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        [Key]
        public int Id { get; set; }
        public int Amount { get; set; }
        public int UserId { get; set; }
        public int ShipperId { get; set; }
        public int AddressId { get; set; }
        public int CardId { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
        public Shipper Shipper { get; set; }
        public Address Address { get; set; }
        public Card Card { get; set; }
    }

}
