using ETicaret.Models;

namespace ETicaret.Repositories.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Category GetById(int id);
    }
}
