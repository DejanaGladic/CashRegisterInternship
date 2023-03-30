using CashRegister.Domain.DTO;
using MediatR;

namespace CashRegister.Domain.Commands
{
    public class UpdateBillCommand : IRequest<bool>
    {
        public BillPostPutDTO _billDTO { get; }
        public UpdateBillCommand(BillPostPutDTO billDTO)
        {
            _billDTO = billDTO;
        }

    }
}
