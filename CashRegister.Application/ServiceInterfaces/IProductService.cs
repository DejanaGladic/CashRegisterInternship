using CashRegister.Domain.Models;

namespace CashRegister.Application.ServiceInterfaces
{
    public interface IProductService
    {
        Task<bool> CreateProduct(Product productDetails);

        Task<IEnumerable<Product>> GetAllProducts();

        Product GetProductById(int productId);
        bool IfProductByIdExists(int productId);

        Task<bool> UpdateProduct(Product productDetails);

        Task<bool> DeleteProduct(int productId);
    }
}
