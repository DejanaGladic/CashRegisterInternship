using CashRegister.Domain.Models;

namespace CashRegister.Application.ServiceInterfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProducts();
        Product GetProductById(int productId);
        bool IfProductByIdExists(int productId);
    }
}
