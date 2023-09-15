using ETicaret.Data;
using ETicaret.Models;
using ETicaret.Repositories.Interfaces;

namespace ETicaret.Repositories.Implementations
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public Category GetById(int id) => _context.Categories.SingleOrDefault(p => p.Id == id);
    }
}