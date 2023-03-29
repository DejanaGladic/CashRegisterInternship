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
        public async Task<bool> CreateProduct(Product product)
        {
            if (product != null)
            {
                await _unitOfWork.ProductRepository.Add(product);

                var result = _unitOfWork.Save();

                if (result > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            if (productId > 0)
            {
                var product = await _unitOfWork.ProductRepository.GetById(productId);
                if (product != null)
                {
                    _unitOfWork.ProductRepository.Delete(product);
                    var result = _unitOfWork.Save();

                    if (result > 0)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var productsList = await _unitOfWork.ProductRepository.GetAll();
            return productsList;
        }

        public async Task<Product> GetProductById(int productId)
        {
            if (productId > 0)
            {
                var product = await _unitOfWork.ProductRepository.GetById(productId);
                if (product != null)
                {
                    return product;
                }
            }
            return null;
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            if (product != null)
            {
                //preuzimanje direktne reference na objekat koji menjamo
                var returnedProduct = await _unitOfWork.ProductRepository.GetById(product.Id);
                if (returnedProduct != null)
                {
                    returnedProduct.Name = product.Name;
                    returnedProduct.Price = product.Price;

                    _unitOfWork.ProductRepository.Update(returnedProduct);

                    var result = _unitOfWork.Save();

                    if (result > 0)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }
    }
}
