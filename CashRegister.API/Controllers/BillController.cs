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

        [HttpPost]
        public async Task<IActionResult> CreateBill(BillDTO billDTO)
        {
            var bill = _mapper.Map<Bill>(billDTO);
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
        public async Task<IActionResult> UpdateBill(BillDTO billDTO)
        {
            var bill = _mapper.Map<Bill>(billDTO);
            if (bill != null)
            {
                var isBillUpdated = await _billService.UpdateBill(bill);
                if (isBillUpdated)
                {
                    return Ok(isBillUpdated);
                }
                return BadRequest();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
