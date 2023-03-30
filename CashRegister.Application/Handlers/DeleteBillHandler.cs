using AutoMapper;
using CashRegister.Application.ServiceInterfaces;
using CashRegister.Domain.Commands;
using CashRegister.Domain.Models;
using MediatR;

namespace CashRegister.Domain.Handlers
{
    public class DeleteBillHandler : IRequestHandler<DeleteBillCommand, bool>
    {
        private IBillService _billService;
        private IMapper _mapper;
        public DeleteBillHandler(IBillService billService, IMapper mapper)
        {
            _billService = billService;
            _mapper = mapper;
        }

        public Task<bool> Handle(DeleteBillCommand request, CancellationToken cancellationToken)
        {
            var isBillDeleted = _billService.DeleteBill(request._billNumber);
            return Task.FromResult(isBillDeleted);
        }
    }
}
