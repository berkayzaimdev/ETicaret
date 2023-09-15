using ETicaret.Data;
using ETicaret.Models;
using ETicaret.Repositories.Interfaces;

namespace ETicaret.Repositories.Implementations
{
    public class OrderItemRepository : GenericRepository<OrderItem>, IOrderItemRepository
    {
        private readonly AppDbContext _context;
        public OrderItemRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
