namespace ETicaret.Models
{
    public class CheckoutViewModel
    {
        public IEnumerable<CartItem>? CartItems { get; set; }
        public IEnumerable<Shipper>? Shippers { get; set; }
        public IEnumerable<Address>? Addresses { get; set; }
        public Address Address { get; set; }
        public Card Card { get; set; }
        public int Amount { get; set; }
        public int ShipperId { get; set; }
        public int SelectedAddress { get; set; }
    }
}
