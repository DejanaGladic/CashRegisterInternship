using CashRegister.Domain.Interfaces;
using CashRegister.Domain.Models;
using CashRegister.Infrastructure.Context;

namespace CashRegister.Infrastructure.Repositories
{
    public class BillRepository : GenericRepository<Bill>, IBillRepository
    {
        public BillRepository(CashRegisterDBContext dbContext) : base(dbContext)
        {

        }
    }
}
