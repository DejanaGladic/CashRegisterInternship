using CashRegister.Domain.Interfaces;
using CashRegister.Domain.Models;
using CashRegister.Infrastructure.Context;

namespace CashRegister.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(CashRegisterDBContext dbContext) : base(dbContext)
        {

        }
    }
}
