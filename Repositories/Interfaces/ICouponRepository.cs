using ETicaret.Models;

namespace ETicaret.Repositories.Interfaces
{
    public interface ICouponRepository : IGenericRepository<Coupon>
    {
        Coupon GetById(int id);
        Coupon GetByCode(string code);
    }
}
