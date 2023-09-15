using Microsoft.AspNetCore.Identity;

namespace ETicaret.Models
{
    public class AppUser : IdentityUser<int>
    {
        public AppUser()
        {
            Addresses = new HashSet<Address>();
            Orders = new HashSet<Order>();
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public Gender Gender { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<Address> Addresses { get; set; }
        public ICollection<Order> Orders { get; set; }
    }

    public enum Gender { Male, Female }
}
