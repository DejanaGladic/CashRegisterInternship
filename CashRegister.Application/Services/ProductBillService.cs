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
        public ProductBillService(IUnitOfWork unitOfWork, IProductService productService, IBillService billService,
            ICalculator calculator)
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

                var productForProductBill = _productService.GetProductById(productBill.ProductId);
                var productsPrice = _calculator.MultiplyOperation(productForProductBill.Price, productBill.ProductQuantity);
                productBill.ProductsPrice = (int)productsPrice;

                if (!_unitOfWork.ProductBillsRepository.IfObjectExists(productBill))
                {                    
                    await _unitOfWork.ProductBillsRepository.Add(productBill);                    
                }
                else
                {
                    UpdateProductBill(productBill, true);
                }

                _billService.CalculateTotalBillPrice(productBill, "adds");

                var result = _unitOfWork.Save();
                if (result > 0) {                   
                    return true;
                }                  
                else
                    return false;
            }           

        }

        public bool UpdateProductBill(ProductBill productBill, bool isFromCreation)
        {
            if (productBill != null)
            {
                //preuzimanje direktne reference na objekat koji menjamo
                var returnedProductBill = _unitOfWork.ProductBillsRepository.GetByProductAndBill(productBill.BillNumber, productBill.ProductId);

                if (returnedProductBill != null)
                {
                    returnedProductBill.ProductId = productBill.ProductId;
                    returnedProductBill.BillNumber = productBill.BillNumber;

                    if (isFromCreation)
                    {
                        returnedProductBill.ProductQuantity += productBill.ProductQuantity;
                        returnedProductBill.ProductsPrice += productBill.ProductsPrice;
                    }
                    else {
                        returnedProductBill.ProductQuantity = productBill.ProductQuantity;
                        returnedProductBill.ProductsPrice = productBill.ProductsPrice;
                    }
                        
                    _unitOfWork.ProductBillsRepository.Update(returnedProductBill);

                    var result = _unitOfWork.Save();

                    if (result > 0)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }

        public bool DeleteProductBill(string billNumber, int productId)
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

        public bool IfProductBillExists(ProductBill productBill)
        {
            if (productBill != null)
            {
                var result = _unitOfWork.ProductBillsRepository.IfObjectExists(productBill);
                return result;
            }
            return false;
        }
    }
}
