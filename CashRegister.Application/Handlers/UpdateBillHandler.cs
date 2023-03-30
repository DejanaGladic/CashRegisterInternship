using AutoMapper;
using CashRegister.Application.ServiceInterfaces;
using CashRegister.Domain.Commands;
using CashRegister.Domain.DTO;
using CashRegister.Domain.Models;
using CashRegister.Domain.Queries;
using MediatR;

namespace CashRegister.Domain.Handlers
{
    public class UpdateBillHandler : IRequestHandler<UpdateBillCommand, bool>
    {
        private IBillService _billService;
        private IMapper _mapper;
        public UpdateBillHandler(IBillService billService, IMapper mapper)
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

        public Task<bool> Handle(UpdateBillCommand request, CancellationToken cancellationToken)
        {
            var bill = _mapper.Map<Bill>(request._billDTO);
            var isBillUpdated = _billService.UpdateBill(bill);
            return Task.FromResult(isBillUpdated);
        }
    }
}
