using MediatR;

namespace CashRegister.Domain.Commands
{
    public class DeleteProductBillCommand : IRequest<bool>
    {
        public string _billNumber { get; }
        public int _productId { get; set; }
        public DeleteProductBillCommand(string billNumber, int productId)
        {
            _billNumber = billNumber;
            _productId = productId;
        }

    }
}
