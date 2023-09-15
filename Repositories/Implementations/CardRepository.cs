using ETicaret.Data;
using ETicaret.Models;
using ETicaret.Repositories.Interfaces;

namespace ETicaret.Repositories.Implementations
{
    public class CardRepository : GenericRepository<Card>, ICardRepository
    {
        private readonly AppDbContext _context;
        public CardRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public Card GetById(int id) => _context.Cards.SingleOrDefault(p => p.Id == id);
    }
}
