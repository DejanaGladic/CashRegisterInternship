using CashRegister.Domain.DTO;
using MediatR;

namespace CashRegister.Domain.Commands
{
    public class DeleteBillCommand : IRequest<bool>
    {
        public string _billNumber { get; }
        public DeleteBillCommand(string billNumber)
        {
            _billNumber = billNumber;
        }

    }
}
