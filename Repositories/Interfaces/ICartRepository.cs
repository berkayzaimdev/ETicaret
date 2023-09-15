using ETicaret.Models;

namespace ETicaret.Repositories.Interfaces
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
        Cart GetByUserId(int userId);
    }
}
