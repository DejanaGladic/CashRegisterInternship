using CashRegister.Domain.Models;

namespace CashRegister.Domain.Interfaces
{
    public interface IProductBillRepository : IGenericRepository<ProductBill>
    {
        ProductBill GetByProductAndBill(string billNumber, int productId);
    }
}
