using AutoMapper;
using CashRegister.Application.ServiceInterfaces;
using CashRegister.Domain.Commands;
using CashRegister.Domain.Models;
using MediatR;

namespace CashRegister.Domain.Handlers
{
    public class CreateBillHandler : IRequestHandler<CreateBillCommand, bool>
    {
        private IBillService _billService;
        private IMapper _mapper;
        public CreateBillHandler(IBillService billService, IMapper mapper)
        {
            _billService = billService;
            _mapper = mapper;
        }

        public Task<bool> Handle(CreateBillCommand request, CancellationToken cancellationToken)
        {
            var bill = _mapper.Map<Bill>(request._billDTO);
            var isBillCreated = _billService.CreateBill(bill);
            return isBillCreated;
        }
    }
}
