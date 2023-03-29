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
        public ProductBillService(IUnitOfWork unitOfWork, IProductService productService, IBillService billService)
        {
            _unitOfWork = unitOfWork;
            _productService = productService;
            _billService = billService;
        }
        public async Task<bool> CreateProductBill(ProductBill productBill)
        {
            var ifProductExists = _productService.IfProductByIdExists(productBill.ProductId);
            var ifBillExists =  _billService.IfBillByIdExists(productBill.BillNumber);

            if (!ifProductExists || !ifBillExists)
                return false;
            else {
                await _unitOfWork.ProductBillsRepository.Add(productBill);

                var result = _unitOfWork.Save();

                if (result > 0)
                    return true;
                else
                    return false;
            }           

        }

        public async Task<bool> UpdateProductBill(ProductBill productBill)
        {
            throw new NotImplementedException();
        }
    }
}
