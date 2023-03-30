using CashRegister.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CashRegister.API.Controllers
{
    [Route("product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductList()
        {
            var query = new GetAllProductsQuery();
            var result = await _mediator.Send(query);
            return result.Count() != 0 ? Ok(result) : NotFound();
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductById(int productId)
        {
            var query = new GetProductByIdQuery(productId);
            var result = await _mediator.Send(query);

            return result != null ? Ok(result) : NotFound();
        }        

    }
}
