using ETicaret.Models;

namespace ETicaret.Repositories.Interfaces
{
    public interface ICardRepository : IGenericRepository<Card>
    {
        Card GetById(int id);
    }
}
