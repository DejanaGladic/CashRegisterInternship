using AutoMapper;
using CashRegister.Application.ServiceInterfaces;
using CashRegister.Domain.DTO;
using CashRegister.Domain.Queries;
using MediatR;

namespace CashRegister.Domain.Handlers
{
    public class GetBillExchangeRateHandlery : IRequestHandler<GetBillExchangeRateQuery, BillDTO>
    {
        private IBillService _billService;
        private IMapper _mapper;
        public GetBillExchangeRateHandlery(IBillService billService, IMapper mapper)
        {
            _billService = billService;
            _mapper = mapper;
        }

        public Task<BillDTO> Handle(GetBillExchangeRateQuery request, CancellationToken cancellationToken)
        {
            var bill = _billService.GetBillExchangeRate(request._billNumber, request._exchangeRate);
            return Task.FromResult(_mapper.Map<BillDTO>(bill));
        }
    }
}
