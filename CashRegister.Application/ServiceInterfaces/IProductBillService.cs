using CashRegister.Domain.Models;

namespace CashRegister.Application.ServiceInterfaces
{
    public interface IProductBillService
    {
        Task<bool> CreateProductBill(ProductBill _productBill);
        bool UpdateProductBill(ProductBill productBill, bool isFromCreation);
        bool IfProductBillExists(ProductBill _productBill);
        bool DeleteProductBill(string billNumber, int productId);
    }
}
