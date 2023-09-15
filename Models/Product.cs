using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETicaret.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; } 
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public int? SubcategoryId { get; set; }

        public List<Comment> Comments { get; set; } = new();
        public Category Category { get; set; }
        public Subcategory? Subcategory { get; set; }
    }
}
