using CashRegister.Application.ServiceInterfaces;
using CashRegister.Domain.Interfaces;
using CashRegister.Domain.Models;

namespace CashRegister.Application.Services
{
    public class ProductService : IProductService
    {
        public IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            var productsList = await _unitOfWork.ProductRepository.GetAll();
            return productsList;
        }

        public Product GetProductById(int productId)
        {
            if (productId > 0)
            {
                var product =  _unitOfWork.ProductRepository.GetById(productId);
                if (product != null)
                {
                    return product;
                }
            }
            return null;
        }

        public bool IfProductByIdExists(int productId)
        {
            if (productId > 0)
            {
                var result = _unitOfWork.ProductRepository.IfExistsById(productId);
                return result;
            }
            return false;
        }
    }
}
