using AutoMapper;
using CashRegister.Application.ServiceInterfaces;
using CashRegister.Application.Services;
using CashRegister.Domain.DTO;
using CashRegister.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CashRegister.API.Controllers
{
    [Route("bill")]
    [ApiController]
    public class BillController : ControllerBase
    {
        public IBillService _billService;
        public IMapper _mapper;
        public BillController(IBillService billService, IMapper mapper)
        {
            _billService = billService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetBillList()
        {
            var billList = await _billService.GetAllBills();
            if (billList == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<BillDTO>>(billList));
        }

        [HttpGet("{billNumber}")]
        public  IActionResult GetBillById(string billNumber)
        {
            var bill = _billService.GetBillById(billNumber);
            if (bill == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<BillDTO>(bill));
        }

        [HttpPost]
        public async Task<IActionResult> CreateBill(BillPostPutDTO billPostPutDTO)
        {
            var bill = _mapper.Map<Bill>(billPostPutDTO);
            var isBillCreated = await _billService.CreateBill(bill);

            if (isBillCreated)
            {
                return Created("/bill","Bill has been created");
            }
            else
            {
                return BadRequest("Bill has not been created");
            }
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
