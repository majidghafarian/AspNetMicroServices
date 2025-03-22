
using Catalog.Api.Entities;
using Catalog.Api.Repostories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.Api.Controllers
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<CatalogController> _logger;
        public CatalogController(ILogger<CatalogController> logger, IProductRepository repository)
        {
            _logger=logger;
            _repository=repository;
        }

        [HttpGet("GetAllProducts")]
        [ProducesResponseType(typeof(IEnumerable<Product>),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            var products = await _repository.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("GetByIdproduct")]
        public async Task<ActionResult<Product>> GetProductById(Guid id)
        {
            var product = await _repository.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound($"Product with id {id} not found");
            }
            return Ok(product);
        }
        [HttpGet("GetProductByCategory/{categoryName}")]
        public async Task<ActionResult<Product>> GetProductByCategory(string categoryName)
        {
            var product = await _repository.GetProductByCategoryAsync(categoryName);
            if (product == null)
            {
                return NotFound($"No products found in category {categoryName}");
            }
            return Ok(product);
        }

        // PUT api/products
        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromBody] Product updatedProduct)
        {
            if (updatedProduct == null)
            {
                return BadRequest("Product is null");
            }

            var success = await _repository.UpdateProductAsync(updatedProduct);
            if (success)
            {
                return NoContent();
            }
            return NotFound($"Product with id {updatedProduct.Id} not found");
        }
        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var success = await _repository.DeleteProductAsync(id);
            if (success)
            {
                return NoContent();
            }
            return NotFound($"Product with id {id} not found");
        }
        // POST api/products
        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Product is null");
            }

            await _repository.AddProductAsync(product);
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }


    }
}
