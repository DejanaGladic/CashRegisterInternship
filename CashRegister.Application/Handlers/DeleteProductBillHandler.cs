using AutoMapper;
using CashRegister.Application.ServiceInterfaces;
using CashRegister.Domain.Commands;
using MediatR;

namespace CashRegister.Domain.Handlers
{
    public class DeleteProductBillHandler : IRequestHandler<DeleteProductBillCommand, bool>
    {
        private IProductBillService _productBillService;

        public DeleteProductBillHandler(IProductBillService productBillService, IMapper mapper)
        {
            _productBillService = productBillService;
        }

        public Task<bool> Handle(DeleteProductBillCommand request, CancellationToken cancellationToken)
        {
            var isProductBillDeleted = _productBillService.DeleteProductBill(request._billNumber, request._productId);
            return Task.FromResult(isProductBillDeleted);
        }
    }
}
