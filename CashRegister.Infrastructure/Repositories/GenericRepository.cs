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

        public async Task<T> GetById(int id)
        {
            return await _cashRegisterDbContext.Set<T>().FindAsync(id);
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
    }
}
