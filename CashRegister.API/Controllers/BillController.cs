using CashRegister.Domain.Commands;
using CashRegister.Domain.DTO;
using CashRegister.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CashRegister.API.Controllers
{
    [Route("bill")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private IMediator _mediator;
        public BillController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetBillList()
        {
            var query = new GetAllBillsQuery();
            var result = await _mediator.Send(query);
            return result.Count() != 0 ? Ok(result) : NotFound();
        }

        [HttpGet("{billNumber}")]
        public async Task<IActionResult> GetBillById(string billNumber)
        {
            var query = new GetBillByIdQuery(billNumber);
            var result = await _mediator.Send(query);

            return result != null ? Ok(result) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBill(BillPostPutDTO billPostPutDTO)
        {
            var query = new CreateBillCommand(billPostPutDTO);
            var result = await _mediator.Send(query);

            return result ? Created("/bill", "Bill has been created") : BadRequest("Bill has not been created");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBill(BillPostPutDTO billPostPutDTO)
        {
            var query = new UpdateBillCommand(billPostPutDTO);
            var result = await _mediator.Send(query);

            return result ? Ok("Bill has been updated") : BadRequest("Bill has not been updated");
        }

        [HttpDelete("{billNumber}")]
        public async Task<IActionResult> DeleteBill(string billNumber)
        {
            var query = new DeleteBillCommand(billNumber);
            var result = await _mediator.Send(query);

            return result ? Ok("Bill has been deleted") : BadRequest("Bill has not been deleted");
        }

        [HttpGet("{billNumber}/{exchangeRate}")]
        public async Task<IActionResult> GetBillExchangeRate(string billNumber, string exchangeRate)
        {
            var query = new GetBillExchangeRateQuery(billNumber, exchangeRate);
            var result = await _mediator.Send(query);

            return result != null ? Ok(result) : NotFound();
        }
    }
}
