using AutoMapper;
using CashRegister.Application.ServiceInterfaces;
using CashRegister.Application.Services;
using CashRegister.Domain.Commands;
using CashRegister.Domain.DTO;
using CashRegister.Domain.Models;
using CashRegister.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CashRegister.API.Controllers
{
    [Route("bill")]
    [ApiController]
    public class BillController : ControllerBase
    {
        public IBillService _billService;
        public IMapper _mapper;
        private IMediator _mediator;
        public BillController(IBillService billService, IMapper mapper, IMediator mediator)
        {
            _billService = billService;
            _mapper = mapper;
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
        public IActionResult UpdateBill(BillPostPutDTO billPostPutDTO)
        {
            var bill = _mapper.Map<Bill>(billPostPutDTO);
            if (bill != null)
            {
                var isBillUpdated = _billService.UpdateBill(bill);
                if (isBillUpdated)
                {
                    return Ok("Bill has been updated");
                }
                return BadRequest("Bill has not been updated");
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{billNumber}")]
        public IActionResult DeleteBill(string billNumber)
        {
            var isBillDeleted = _billService.DeleteBill(billNumber);

            if (isBillDeleted)
            {
                return Ok("Bill has been deleted");
            }
            else
            {
                return BadRequest("Bill has not been deleted");
            }
        }

        [HttpGet("{billNumber}/{exchangeRate}")]
        public IActionResult GetBillExchangeRate(string billNumber, string exchangeRate)
        {
            var bill = _billService.GetBillExchangeRate(billNumber, exchangeRate);
            if (bill == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<BillDTO>(bill));
        }
    }
}
