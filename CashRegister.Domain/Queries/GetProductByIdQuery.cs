using CashRegister.Application.DTO;
using MediatR;

namespace CashRegister.Domain.Queries
{
    public class GetProductByIdQuery : IRequest<ProductDTO>
    {
        public int _productId { get;  }
        public GetProductByIdQuery(int productId)
        {
            _productId = productId;
        }
    }
}
