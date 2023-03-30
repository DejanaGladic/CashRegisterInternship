using AutoMapper;
using CashRegister.Application.ServiceInterfaces;
using CashRegister.Domain.DTO;
using CashRegister.Domain.Queries;
using MediatR;

namespace CashRegister.Domain.Handlers
{
    public class GetBillByIdHandler : IRequestHandler<GetBillByIdQuery, BillDTO>
    {
        private IBillService _billService;
        private IMapper _mapper;
        public GetBillByIdHandler(IBillService billService, IMapper mapper)
        {
            _billService = billService;
            _mapper = mapper;
        }
        public Task<BillDTO> Handle(GetBillByIdQuery request, CancellationToken cancellationToken)
        {
            var bill = _billService.GetBillById(request._billNumber);
            return Task.FromResult(_mapper.Map<BillDTO>(bill));
        }
    }
}
