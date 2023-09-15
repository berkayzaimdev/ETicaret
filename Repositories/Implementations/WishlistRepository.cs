using ETicaret.Data;
using ETicaret.Models;
using ETicaret.Repositories.Interfaces;

namespace ETicaret.Repositories.Implementations
{
    public class WishlistRepository : GenericRepository<Wishlist>, IWishlistRepository
    {
        private readonly AppDbContext _context;
        public WishlistRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public List<Wishlist> GetByUserId(int userId) => GetAll().Where(w => w.UserId == userId).ToList();
        public Wishlist GetByUserIdAndProductId(int userId, int productId) => GetAll().SingleOrDefault(w => w.UserId == userId && w.ProductId == productId);
    }
}
