using AutoMapper;
using CashRegister.Application.DTO;
using CashRegister.Application.ServiceInterfaces;
using CashRegister.Domain.Queries;
using MediatR;

namespace CashRegister.Domain.Handlers
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductDTO>
    {
        private IProductService _productService;
        private IMapper _mapper;
        public GetProductByIdHandler(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }
        public Task<ProductDTO> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = _productService.GetProductById(request._productId);
            return Task.FromResult(_mapper.Map<ProductDTO>(product));
        }
    }
}
