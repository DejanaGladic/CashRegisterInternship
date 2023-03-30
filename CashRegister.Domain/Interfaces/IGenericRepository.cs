namespace CashRegister.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        T GetById(int id);
        T GetByStringId(string id);
        bool IfObjectExists(T entity);
        bool IfExistsById(int id);
        bool IfExistsByStringId(string id);
        Task<List<T>> GetAll();
        Task Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
