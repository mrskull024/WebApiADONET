using Microsoft.AspNetCore.Mvc;
using TestingApiADONET.Models;
using TestingApiADONET.Services;

namespace TestingApiADONET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly ProductRepository _productRepository;
        public ProductController(ILogger<ProductController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _productRepository = new ProductRepository(configuration);
        }

        [HttpGet("List")]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await _productRepository.GetAllProducts());
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productRepository.GetProduct(id);

            if (product is null || product.Id == 0)
                return NotFound("El producto solicitado no existe");
            return Ok(product);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddProduct(Product product)
        {
            await _productRepository.AddProduct(product);
            return NoContent();
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            var validateProduct = _productRepository.GetProduct(id).Result;
            if (validateProduct is null || validateProduct.Id == 0)
                return NotFound($"El producto con id: {id} a actualizar no existe");

            product.Id = id;
            await _productRepository.UpdateProduct(product);
            return NoContent();
        }

        [HttpDelete("Deactivate/{id}")]
        public async Task<IActionResult> DeactivateProduct(int id)
        {
            var validateProduct = _productRepository.GetProduct(id).Result;
            if (validateProduct is null || validateProduct.Id == 0)
                return NotFound($"El producto con id: {id} a desactivar no existe");

            await _productRepository.DeactivateProduct(id);
            return NoContent();
        }
    }
}
