using AutoMapper;
using CashRegister.Application.DTO;
using CashRegister.Application.ServiceInterfaces;
using CashRegister.Domain.Queries;
using MediatR;

namespace CashRegister.Domain.Handlers
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, List<ProductDTO>>
    {
        private IProductService _productService;
        private IMapper _mapper;
        public GetAllProductsHandler(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }
        public async Task<List<ProductDTO>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var productslList = await _productService.GetAllProducts();
            return _mapper.Map<List<ProductDTO>>(productslList);
        }
    }
}
