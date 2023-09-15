using ETicaret.Models;

namespace ETicaret.Repositories.Interfaces
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        List<Comment> GetByProductId(int productId);
    }
}
