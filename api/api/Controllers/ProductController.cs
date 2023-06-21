using api.DTOs.ProductR;
using api.Service.ProductS; 
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }



        [HttpGet("[action]")]
        public async Task<IActionResult> GetProduct()
            => Ok(await _productService.GetProductAsync());
        
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateUpdateProduct([FromForm] ProductRequest productRequest)
            => Ok(await _productService.CreateAndUpdateAsync(productRequest));

        [HttpDelete("[action]")]
        public async Task<IActionResult> Remove(int id)
            => Ok(await _productService.RemoveAsync(id));



    }
}
