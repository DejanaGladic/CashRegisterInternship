using CashRegister.Domain.Models;
using MediatR;

namespace CashRegister.Domain.Commands
{
    public class CalculateBillPriceCommand : IRequest<int>
    {
        public ProductBill _productBill { get; set; }
        public string _typeOfCalculation { get; set; }
        public CalculateBillPriceCommand(ProductBill productBill, string typeOfCalculation)
        {
            _productBill = productBill;
            _typeOfCalculation = typeOfCalculation;
        }

    }
}
