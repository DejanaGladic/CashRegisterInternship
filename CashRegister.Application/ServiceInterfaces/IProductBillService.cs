using CashRegister.Domain.Models;

namespace CashRegister.Application.ServiceInterfaces
{
    public interface IProductBillService
    {
        Task<bool> CreateProductBill(ProductBill _productBill);
       // Task<bool> UpdateProductBill(ProductBill _productBill);
        bool IfProductBillExists(ProductBill _productBill);
        bool DeleteProductBill(string billNumber, int productId);
    }
}
