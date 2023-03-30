using CashRegister.Domain.DTO;
using MediatR;

namespace CashRegister.Domain.Queries
{
    public class GetAllBillsQuery : IRequest<List<BillDTO>>
    {
    }
}
