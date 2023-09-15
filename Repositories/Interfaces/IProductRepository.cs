using ETicaret.Models;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Repositories.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Product GetById(int id);
    }
}
