using CashRegister.Domain.Interfaces;
using CashRegister.Domain.Models;
using CashRegister.Infrastructure.Context;

namespace CashRegister.Infrastructure.Repositories
{
    public class ProductBillRepository : GenericRepository<ProductBill>, IProductBillRepository
    {
        public ProductBillRepository(CashRegisterDBContext dbContext) : base(dbContext)
        {

        }
    }
}
