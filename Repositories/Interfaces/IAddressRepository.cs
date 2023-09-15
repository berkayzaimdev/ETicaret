using ETicaret.Models;

namespace ETicaret.Repositories.Interfaces
{
    public interface IAddressRepository : IGenericRepository<Address>
    {
        Address GetById(int id);
    }
}
