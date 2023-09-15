using ETicaret.Models;

namespace ETicaret.Repositories.Interfaces
{
    public interface IWishlistRepository : IGenericRepository<Wishlist>
    {
        List<Wishlist> GetByUserId(int userId);
        Wishlist GetByUserIdAndProductId(int userId, int productId);
    }
}
