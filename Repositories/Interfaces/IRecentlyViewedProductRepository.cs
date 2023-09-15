using ETicaret.Models;

namespace ETicaret.Repositories.Interfaces
{
    public interface IRecentlyViewedProductRepository : IGenericRepository<RecentlyViewedProduct>
    {
        RecentlyViewedProduct GetById(int id);
        List<RecentlyViewedProduct> GetUserRVPsByUserId(int id);
    }
}
