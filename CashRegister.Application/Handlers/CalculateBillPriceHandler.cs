using AutoMapper;
using CashRegister.Application.ServiceInterfaces;
using CashRegister.Domain.Commands;
using MediatR;

namespace CashRegister.Domain.Handlers
{
    public class CalculateBillPriceHandler : IRequestHandler<CalculateBillPriceCommand, int>
    {
        private IBillService _billService;
        public CalculateBillPriceHandler(IBillService billService, IMapper mapper)
        {
            _billService = billService;
        }

        public Task<int> Handle(CalculateBillPriceCommand request, CancellationToken cancellationToken)
        {
            var result = _billService.CalculateTotalBillPrice(request._productBill, request._typeOfCalculation);
            return Task.FromResult(result);
        }
    }
}
