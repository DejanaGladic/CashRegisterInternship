using CashRegister.Application.ServiceInterfaces;
using CashRegister.Domain.Interfaces;
using CashRegister.Domain.Models;

namespace CashRegister.Application.Services
{
    public class ProductBillService : IProductBillService
    {
        private IUnitOfWork _unitOfWork;
        private IProductService _productService;
        private IBillService _billService;
        private ICalculator _calculator;
        public ProductBillService(IUnitOfWork unitOfWork, IProductService productService, IBillService billService, ICalculator calculator)
        {
            _unitOfWork = unitOfWork;
            _productService = productService;
            _billService = billService;
            _calculator = calculator;
        }
        public async Task<bool> CreateProductBill(ProductBill productBill)
        {
            var ifProductExists = _productService.IfProductByIdExists(productBill.ProductId);
            var ifBillExists =  _billService.IfBillByIdExists(productBill.BillNumber);

            if (!ifProductExists || !ifBillExists)
                return false;
            else {          
                await _unitOfWork.ProductBillsRepository.Add(productBill);

                var productsPrice = _calculator.MultiplyOperation(productBill.Product.Price, productBill.ProductQuantity);
                productBill.ProductsPrice = (int)productsPrice;
                _billService.CalculateTotalBillPrice(productBill, "adds");
                var result = _unitOfWork.Save();
                if (result > 0) {                   
                    return true;
                }                  
                else
                    return false;              
            }           

        }

        public async Task<bool> DeleteProductBill(string billNumber, int productId)
        {
            if (productId > 0 && billNumber != null)
            {
                var productBill = _unitOfWork.ProductBillsRepository.GetByProductAndBill(billNumber,productId);

                if (productBill != null)
                {
                    _billService.CalculateTotalBillPrice(productBill, "subtract");
                    _unitOfWork.ProductBillsRepository.Delete(productBill);
                    var result = _unitOfWork.Save();
                    
                    if (result > 0) {                      
                        return true;
                    }
                    else
                        return false;
                }
            }
            return false;
        }

        public Task<bool> UpdateProductBill(ProductBill _productBill)
        {
            throw new NotImplementedException();
        }
    }
}
