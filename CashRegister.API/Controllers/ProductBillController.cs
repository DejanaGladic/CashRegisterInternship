using AutoMapper;
using CashRegister.Application.ServiceInterfaces;
using CashRegister.Application.Services;
using CashRegister.Domain.Commands;
using CashRegister.Domain.DTO;
using CashRegister.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CashRegister.API.Controllers
{
    [Route("productBill")]
    [ApiController]
    public class ProductBillController : ControllerBase
    {
       /* public IProductBillService _productBillService;
        public IMapper _mapper;
        public ProductBillController(IProductBillService productBillService, IMapper mapper)
        {
            _productBillService = productBillService;
            _mapper = mapper;
        }*/

        private IMediator _mediator;
        public ProductBillController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductBill(ProductBillPostPutDTO productBillPostPutDTO)
        {
            var query = new CreateProductBillCommand(productBillPostPutDTO);
            var result = await _mediator.Send(query);

            return result ? Created("/bill", "Product bill has been created") 
                : BadRequest("Product bill has not been created");

        }

        [HttpDelete("/{billNumber}/{productId}")]
        public async Task<IActionResult> DeleteProductBill(string billNumber, int productId)
        {
            var query = new DeleteProductBillCommand(billNumber, productId);
            var result = await _mediator.Send(query);

            return result ? Ok("Product has been deleted") : BadRequest("Product has not been deleted");
        }
    }
}
