using ETicaret.Data;
using ETicaret.Models;
using ETicaret.Repositories.Interfaces;

namespace ETicaret.Repositories.Implementations
{
    public class ShipperRepository : GenericRepository<Shipper>, IShipperRepository
    {
        private readonly AppDbContext _context;
        public ShipperRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public Shipper GetById(int id) => _context.Shippers.SingleOrDefault(obj => obj.Id == id);
    }
}
