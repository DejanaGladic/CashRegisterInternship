using CashRegister.Domain.Interfaces;
using CashRegister.Infrastructure.Context;

namespace CashRegister.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CashRegisterDBContext _dbContext;
        public IProductRepository ProductRepository { get; }
        public IBillRepository BillRepository { get; }
        public IProductBillRepository ProductBillsRepository { get; }

        public UnitOfWork(CashRegisterDBContext dbContext,
                            IProductRepository productRepository,
                            IBillRepository billRepository,
                            IProductBillRepository productBillRepository)
        {
            _dbContext = dbContext;
            ProductRepository = productRepository;
            BillRepository = billRepository;
            ProductBillsRepository = productBillRepository;
        }

        public int Save()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }
    }
}
