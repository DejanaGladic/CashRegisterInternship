using CashRegister.Domain.DTO;
using MediatR;

namespace CashRegister.Domain.Commands
{
    public class CreateBillCommand : IRequest<bool>
    {
        public BillPostPutDTO _billDTO { get; }
        public CreateBillCommand(BillPostPutDTO billDTO)
        {
            _billDTO = billDTO;
        }

    }
}
