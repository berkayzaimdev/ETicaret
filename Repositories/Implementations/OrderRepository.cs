using ETicaret.Data;
using ETicaret.Models;
using ETicaret.Repositories.Interfaces;

namespace ETicaret.Repositories.Implementations
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly AppDbContext _context;
        public OrderRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
