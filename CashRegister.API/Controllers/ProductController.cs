using AutoMapper;
using CashRegister.Application.DTO;
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
        public IMapper _mapper;
        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductList()
        {
            var productsList = await _productService.GetAllProducts();
            if (productsList == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<List<ProductDTO>>(productsList));
        }

        [HttpGet("{productId}")]
        public  IActionResult GetProductById(int productId)
        {
            var productById = _productService.GetProductById(productId);

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
        public async Task<IActionResult> CreateProduct(ProductDTO productDTO)
        {
            var product = _mapper.Map<Product>(productDTO);
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
        public IActionResult UpdateProduct(Product product)
        {
            if (product != null)
            {
                var isProductCreated = _productService.UpdateProduct(product);
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
        public IActionResult DeleteProduct(int productId)
        {
            var isProductCreated = _productService.DeleteProduct(productId);

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
