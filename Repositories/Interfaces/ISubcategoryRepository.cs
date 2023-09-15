using ETicaret.Models;

namespace ETicaret.Repositories.Interfaces
{
    public interface ISubcategoryRepository : IGenericRepository<Subcategory>
    {
        Subcategory GetById(int id);
        List<Subcategory> GetByCategoryId(int categoryId);
    }
}
