using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETicaret.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string Detail { get; set; }
        public int Rating { get; set; }
        public DateTime CommentTime { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [ForeignKey("UserId")]
        public AppUser User { get; set; }
    }
}
