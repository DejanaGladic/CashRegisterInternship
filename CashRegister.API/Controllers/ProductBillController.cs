using AutoMapper;
using CashRegister.Application.ServiceInterfaces;
using CashRegister.Domain.DTO;
using CashRegister.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CashRegister.API.Controllers
{
    [Route("productBill")]
    [ApiController]
    public class ProductBillController : ControllerBase
    {
        public IProductBillService _productBillService;
        public IMapper _mapper;
        public ProductBillController(IProductBillService productBillService, IMapper mapper)
        {
            _productBillService = productBillService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductBill(ProductBillDTO productBillDTO)
        {
            var bill = _mapper.Map<ProductBill>(productBillDTO);
            var isProductBillCreated = await _productBillService.CreateProductBill(bill);

            if (isProductBillCreated)
            {
                return Created("/productBill", "Product bill has been created");
            }
            else
            {
                return BadRequest("Product bill has not been created");
            }
        }
    }
}
