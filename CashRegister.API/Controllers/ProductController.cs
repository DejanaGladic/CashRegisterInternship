using CashRegister.Application.ServiceInterfaces;
using CashRegister.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CashRegister.API.Controllers
{
    [Route("product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductList()
        {
            var productsList = await _productService.GetAllProducts();
            if (productsList == null)
            {
                return NotFound();
            }
            return Ok(productsList);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductById(int productId)
        {
            var productById = await _productService.GetProductById(productId);

            if (productById != null)
            {
                return Ok(productById);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            var isProductCreated = await _productService.CreateProduct(product);

            if (isProductCreated)
            {
                return Ok(isProductCreated);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            if (product != null)
            {
                var isProductCreated = await _productService.UpdateProduct(product);
                if (isProductCreated)
                {
                    return Ok(isProductCreated);
                }
                return BadRequest();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var isProductCreated = await _productService.DeleteProduct(productId);

            if (isProductCreated)
            {
                return Ok(isProductCreated);
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
