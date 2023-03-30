using AutoMapper;
using CashRegister.Application.ServiceInterfaces;
using CashRegister.Domain.Commands;
using CashRegister.Domain.Models;
using MediatR;

namespace CashRegister.Domain.Handlers
{
    public class CreateProductBillHandler : IRequestHandler<CreateProductBillCommand, bool>
    {
        private IProductBillService _productBillService;
        private IMapper _mapper;
        public CreateProductBillHandler(IProductBillService productBillService, IMapper mapper)
        {
            _productBillService = productBillService;
            _mapper = mapper;
        }

        public Task<bool> Handle(CreateProductBillCommand request, CancellationToken cancellationToken)
        {
            var productBill = _mapper.Map<ProductBill>(request._productBillDTO);
            var isProductBillCreated = _productBillService.CreateProductBill(productBill);
            return isProductBillCreated;
        }
    }
}
