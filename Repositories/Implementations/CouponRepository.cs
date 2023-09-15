using ETicaret.Data;
using ETicaret.Models;
using ETicaret.Repositories.Interfaces;

namespace ETicaret.Repositories.Implementations
{
    public class CouponRepository : GenericRepository<Coupon>, ICouponRepository
    {
        private readonly AppDbContext _context;
        public CouponRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public Coupon GetById(int id) => GetAll().SingleOrDefault(c => c.Id == id);
        public Coupon GetByCode(string code) => GetAll().SingleOrDefault(c => c.Code == code);
    }
}
