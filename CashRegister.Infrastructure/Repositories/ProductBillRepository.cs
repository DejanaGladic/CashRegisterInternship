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

        public ProductBill GetByProductAndBill(string billNumber, int productId)
        {
            return _cashRegisterDbContext.Set<ProductBill>().Find(billNumber, productId);
        }
    }
}
