using AutoMapper;
using CashRegister.Application.ServiceInterfaces;
using CashRegister.Domain.DTO;
using CashRegister.Domain.Queries;
using MediatR;

namespace CashRegister.Domain.Handlers
{
    public class GetAllBillsHandler : IRequestHandler<GetAllBillsQuery, List<BillDTO>>
    {
        private IBillService _billService;
        private IMapper _mapper;
        public GetAllBillsHandler(IBillService billService, IMapper mapper)
        {
            _billService = billService;
            _mapper = mapper;
        }
        public async Task<List<BillDTO>> Handle(GetAllBillsQuery request, CancellationToken cancellationToken)
        {
            var billList = await _billService.GetAllBills();
            return _mapper.Map<List<BillDTO>>(billList);
        }
    }
}
