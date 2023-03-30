using CashRegister.Domain.Models;

namespace CashRegister.Application.ServiceInterfaces
{
    public interface IProductService
    {
        Task<bool> CreateProduct(Product productDetails);

        Task<List<Product>> GetAllProducts();

        Product GetProductById(int productId);
        bool IfProductByIdExists(int productId);

        bool UpdateProduct(Product productDetails);

        bool DeleteProduct(int productId);
    }
}
