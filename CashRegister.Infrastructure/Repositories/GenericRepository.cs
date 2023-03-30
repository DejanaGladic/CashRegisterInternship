using CashRegister.Domain.Interfaces;
using CashRegister.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CashRegister.Infrastructure.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly CashRegisterDBContext _cashRegisterDbContext;

        protected GenericRepository(CashRegisterDBContext context)
        {
            _cashRegisterDbContext = context;
        }

        public T GetById(int id)
        {
            return  _cashRegisterDbContext.Set<T>().Find(id);
        }

        public T GetByStringId(string id)
        {
            return _cashRegisterDbContext.Set<T>().Find(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _cashRegisterDbContext.Set<T>().ToListAsync();
        }

        public async Task Add(T entity)
        {
            await _cashRegisterDbContext.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
             _cashRegisterDbContext.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
             _cashRegisterDbContext.Set<T>().Update(entity);
        }

        public bool IfExistsById(int id)
        {
            var obj = _cashRegisterDbContext.Set<T>().Find(id);
            if (obj != null)
                return true;
            return false;

        }

        public bool IfExistsByStringId(string id)
        {
            var obj = _cashRegisterDbContext.Set<T>().Find(id);
            if (obj != null)
                return true;
            return false;
        }

        public bool IfObjectExists(T entity)
        {
            var result = _cashRegisterDbContext.Set<T>().Contains(entity);
            return result;
        }

    }
}
