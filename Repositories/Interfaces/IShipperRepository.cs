using ETicaret.Models;

namespace ETicaret.Repositories.Interfaces
{
    public interface IShipperRepository : IGenericRepository<Shipper>
    {
        Shipper GetById(int id);
    }
}
