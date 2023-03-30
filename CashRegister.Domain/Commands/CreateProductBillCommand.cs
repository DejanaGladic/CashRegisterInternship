using CashRegister.Domain.DTO;
using MediatR;

namespace CashRegister.Domain.Commands
{
    public class CreateProductBillCommand : IRequest<bool>
    {
        public ProductBillPostPutDTO _productBillDTO { get; }
        public CreateProductBillCommand(ProductBillPostPutDTO productBillDTO)
        {
             _productBillDTO = productBillDTO;
        }

    }
}
