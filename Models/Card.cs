using System.ComponentModel.DataAnnotations;

namespace ETicaret.Models
{
    public class Card
    {
        [Key]
        public int Id { get; set; }
        public string Number { get; set; }
        public string Owner { get; set; }
        public string ExpirationMonth { get; set; }
        public string ExpirationYear { get; set; }
        public int SecurityCode { get; set; } 
    }
}
