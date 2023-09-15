using System.ComponentModel.DataAnnotations;

namespace ETicaret.Models
{
    public class Shipper
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
