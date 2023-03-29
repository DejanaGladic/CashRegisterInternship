using CashRegister.Domain.Models;

namespace CashRegister.Application.ServiceInterfaces
{
    public interface IBillService
    {
        Task<bool> CreateBill(Bill bill);
        Task<bool> UpdateBill(Bill bill);

        Task<bool> DeleteBill(string billNumber);
    }
}
