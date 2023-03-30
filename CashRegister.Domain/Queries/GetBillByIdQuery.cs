using CashRegister.Domain.DTO;
using MediatR;

namespace CashRegister.Domain.Queries
{
    public class GetBillByIdQuery : IRequest<BillDTO>
    {
        public string _billNumber { get;  }
        public GetBillByIdQuery(string billNumber) {
            _billNumber = billNumber;
        }
    }
}
