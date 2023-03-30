using CashRegister.Application.DTO;
using MediatR;

namespace CashRegister.Domain.Queries
{
    public class GetAllProductsQuery : IRequest<List<ProductDTO>>
    {
    }
}
