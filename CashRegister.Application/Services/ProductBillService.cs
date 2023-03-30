using CashRegister.Application.ServiceInterfaces;
using CashRegister.Domain.Commands;
using CashRegister.Domain.Interfaces;
using CashRegister.Domain.Models;
using CashRegister.Domain.Queries;
using MediatR;

namespace CashRegister.Application.Services
{
    public class ProductBillService : IProductBillService
    {
        private IUnitOfWork _unitOfWork;
        private ICalculator _calculator;
        private IMediator _mediator;
        public ProductBillService(IUnitOfWork unitOfWork, IProductService productService, IBillService billService,
            ICalculator calculator, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _calculator = calculator;
            _mediator = mediator;
        }
        public async Task<bool> CreateProductBill(ProductBill productBill)
        {
            var ifProductExists = new GetProductByIdQuery(productBill.ProductId);
            var product = await _mediator.Send(ifProductExists);

            var ifBillExists = new GetBillByIdQuery(productBill.BillNumber);
            var bill = await _mediator.Send(ifBillExists);

            if (product == null || bill == null)
                return false;
            else {

                var query = new GetProductByIdQuery(productBill.ProductId);
                var queryResult = await _mediator.Send(query);

                var productsPrice = _calculator.MultiplyOperation(queryResult.Price, productBill.ProductQuantity);
                productBill.ProductsPrice = (int)productsPrice;

                if (!_unitOfWork.ProductBillsRepository.IfObjectExists(productBill))
                {                    
                    await _unitOfWork.ProductBillsRepository.Add(productBill);                    
                }
                else
                {
                    UpdateProductBill(productBill, true);
                }

                var calculateQuery = new CalculateBillPriceCommand(productBill, "adds");
                await _mediator.Send(calculateQuery);

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
                    var calculateQuery = new CalculateBillPriceCommand(productBill, "adds");
                    _mediator.Send(calculateQuery);

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
