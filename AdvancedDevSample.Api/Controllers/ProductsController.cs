using AdvancedDevSample.Domain.Exceptions;
using AdvancedDevSample.Application.Services;
using Microsoft.AspNetCore.Mvc;
using AdvancedDevSample.Application.Exceptions;
using AdvancedDevSample.Application.DTOs;

namespace AdvancedDevSample.Api.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }


        // GET: api/products
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        // GET: api/products/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var product = await _productService.GetProductAsync(id);
                return Ok(product);
            }
            catch (Exception ex) // Idéalement catch NotFoundException
            {
                return NotFound(ex.Message);
            }
        }

        // POST: api/products
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var id = await _productService.CreateProductAsync(request);
                return CreatedAtAction(nameof(GetById), new { id = id }, new { id = id });
            }
            catch (DomaineException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/products/{id}/price
        [HttpPut("{id}/price")]
        public async Task<IActionResult> ChangePrice(Guid id, [FromBody] ChangePriceRequest request)
        {
            try
            {
                await _productService.ChangeProductPriceAsync(id, request.NewPrice);
                return NoContent();
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/products/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _productService.DeleteProductAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


       /* [HttpPut("{id}/price")] deja utilisé en haut
        public IActionResult ChangePrice(Guid id, [FromBody] ChangePriceRequest request)
        {
            try
            {
                _productService.ChangeProductPrice(id, request.NewPrice);
                return NoContent();
            }
            catch (ApplicationServiceException ex)
            {
                return NotFound(ex.Message);
            }
            catch (DomaineException ex)                                                                                                                                                                                                                                                                                                                                                                                               
            {
                return BadRequest(ex.Message);
            }
        }*/
    }
}
