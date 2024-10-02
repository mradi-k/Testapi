using Microsoft.AspNetCore.Mvc;
using Testapi.Model;
using Testapi.Services;

namespace Testapi.Controllers
{
   
    public class ProductsController : Controller
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("/Api/GetProducts")]
        public async Task<ActionResult<List<Product>>> GetProducts() =>
            await _productService.GetAsync();

        [HttpGet]
        [Route("/Api/GetProductById")]
        public async Task<ActionResult<Product>> GetProductById([FromQuery]string id)
        {
            var product = await _productService.GetAsync(id);
            if (product == null)
                return NotFound();

            return Json(product);
        }

        [HttpPost]
        [Route("/Api/SaveProduct")]
        public async Task<IActionResult> SaveProduct([FromBody]Product product)
        {
            await _productService.CreateAsync(product);
            return Json( new { id = product.Id }, product);
        }


        [HttpDelete]
        [Route("/Api/DeleteProduct")]
        public async Task<IActionResult> DeleteProduct([FromQuery]string id)
        {
            var product = await _productService.GetAsync(id);
            if (product == null)
                return NotFound();

            await _productService.DeleteAsync(id);
            return NoContent();
        }
    }
}
