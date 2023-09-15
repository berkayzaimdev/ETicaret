namespace ETicaret.Models
{
    public class EditViewModel
    {
        public string username { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
