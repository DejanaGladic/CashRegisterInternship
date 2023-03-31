using CashRegister.Domain.Interfaces;
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

        int CalculateTotalBillPrice(ProductBill productBill, string typeOfCalculation);

        void SetUnitOfWork(IUnitOfWork unitOfWork);

    }
}
