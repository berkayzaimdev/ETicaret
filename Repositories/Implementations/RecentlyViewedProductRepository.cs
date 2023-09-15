using ETicaret.Models;
using ETicaret.Data;
using ETicaret.Repositories.Interfaces;

namespace ETicaret.Repositories.Implementations
{
    public class RecentlyViewedProductRepository : GenericRepository<RecentlyViewedProduct>, IRecentlyViewedProductRepository
    {
        private readonly AppDbContext _context;
        public RecentlyViewedProductRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public RecentlyViewedProduct GetById(int id) => _context.RecentlyViewedProducts.SingleOrDefault(rvp => rvp.Id == id);
        public List<RecentlyViewedProduct> GetUserRVPsByUserId(int id) =>_context.RecentlyViewedProducts.Where(rvp => rvp.UserId == id).ToList();
    }
}
