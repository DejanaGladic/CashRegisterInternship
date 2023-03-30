using AutoMapper;
using CashRegister.Application.ServiceInterfaces;
using CashRegister.Application.Services;
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
        public async Task<IActionResult> CreateProductBill(ProductBillPostPutDTO productBillPostPutDTO)
        {
            var bill = _mapper.Map<ProductBill>(productBillPostPutDTO);
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

        [HttpDelete("/{billNumber}/{productId}")]
        public IActionResult DeleteProductBill(string billNumber, int productId)
        {
            var isBillProductDeleted = _productBillService.DeleteProductBill(billNumber, productId);

            if (isBillProductDeleted)
            {
                return Ok("Product has been deleted");
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
