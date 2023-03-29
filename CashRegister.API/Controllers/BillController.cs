﻿using AutoMapper;
using CashRegister.Application.DTO;
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
        public async Task<IActionResult> GetBillById(string billNumber)
        {
            var bill = await _billService.GetBillById(billNumber);
            if (bill == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<BillDTO>(bill));
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
        public async Task<IActionResult> DeleteBill(string billNumber)
        {
            var isBillDeleted = await _billService.DeleteBill(billNumber);

            if (isBillDeleted)
            {
                return Ok("Bill has been deleted");
            }
            else
            {
                return BadRequest("Bill has not been deleted");
            }
        }
    }
}
