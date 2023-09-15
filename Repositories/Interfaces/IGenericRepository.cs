namespace ETicaret.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        void Add(T obj);
        IEnumerable<T> GetAll();
        void Remove(T obj);
        void Update(T obj);
    }
}
