using ETicaret.Data;
using ETicaret.Models;
using ETicaret.Repositories.Interfaces;

namespace ETicaret.Repositories.Implementations
{
    public class CartItemRepository : GenericRepository<CartItem>, ICartItemRepository
    {
        private readonly AppDbContext _context;
        public CartItemRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public CartItem GetById(int id) => GetAll().SingleOrDefault(ci => ci.Id == id);
        public IEnumerable<CartItem> GetByCartId(int cartId) => GetAll().Where(ci => ci.CartId == cartId);
        public CartItem GetByCartIdAndProductId(int cartId, int productId) => GetAll().SingleOrDefault(ci => ci.CartId == cartId && ci.ProductId == productId);
    }
}
