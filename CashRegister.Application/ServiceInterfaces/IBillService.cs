using CashRegister.Domain.Models;

namespace CashRegister.Application.ServiceInterfaces
{
    public interface IBillService
    {
        Task<bool> CreateBill(Bill bill);
        bool UpdateBill(Bill bill);
        bool DeleteBill(string billNumber);
        Task<List<Bill>> GetAllBills();

        Bill GetBillById(string billNumber);

        Bill GetBillExchangeRate(string billNumber, string exchangeRate);

        bool IfBillByIdExists(string billNumber);

        void CalculateTotalBillPrice(ProductBill productBill, string typeOfCalculation);
    }
}
