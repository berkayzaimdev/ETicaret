using ETicaret.Data;
using ETicaret.Models;
using ETicaret.Repositories.Interfaces;

namespace ETicaret.Repositories.Implementations
{
    public class SubcategoryRepository : GenericRepository<Subcategory>, ISubcategoryRepository
    {
        private readonly AppDbContext _context;
        public SubcategoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public Subcategory GetById(int id) => _context.Subcategories.SingleOrDefault(p => p.Id == id);
        public List<Subcategory> GetByCategoryId(int categoryId) => _context.Subcategories.Where(x=>x.CategoryId == categoryId).ToList();
    }
}