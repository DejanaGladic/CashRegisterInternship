using CashRegister.Domain.DTO;
using MediatR;

namespace CashRegister.Domain.Queries
{
    public class GetBillExchangeRateQuery : IRequest<BillDTO>
    {
        public string _billNumber { get; set; }
        public string _exchangeRate { get; set; }
        public GetBillExchangeRateQuery(string billNumber, string exchangeRate) {
            _billNumber = billNumber;
            _exchangeRate = exchangeRate;
        }
    }
}
