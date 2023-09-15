using ETicaret.Models;

namespace ETicaret.Repositories.Interfaces
{
    public interface ICartItemRepository : IGenericRepository<CartItem>
    {
        CartItem GetById(int id);
        IEnumerable<CartItem> GetByCartId(int cartId);
        CartItem GetByCartIdAndProductId(int cartId, int productId);
    }
}
