using System;
using System.Linq;
using BetaBrew.Services.Product;
using BetaBrew.Web.Serialization;
using BetaBrew.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BetaBrew.Web.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }
        
        [HttpGet("/api/product")]
        public ActionResult GetProducts()
        {
            _logger.LogInformation("Getting all products");
            
            var products = _productService.GetAllProducts();
            var productViewModels = products.Select(product => ProductMapper.SerializeProductModel(product));
            
            return Ok(productViewModels);
        }
        
        [HttpPatch("/api/product/{id}")]
        public ActionResult ArchiveProduct(int id)
        {
            _logger.LogInformation($"Archiving Product {id}");
            
            var product = _productService.ArchiveProduct(id);
            
            return Ok(product);
        }

        [HttpPost("/api/product/")]
        public ActionResult CreateProduct([FromBody] ProductModel product)
        {
               
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Creating Product");

            product.CreatedOn = DateTime.UtcNow;
            product.UpdatedOn = DateTime.UtcNow;

            var productData = ProductMapper.SerializeProductModel(product);
            var newProduct = _productService.CreateProduct(productData);

            return Ok(newProduct);
        }
    }
}
 