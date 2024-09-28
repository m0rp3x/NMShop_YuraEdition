// Server/Controllers/ProductsController.cs
using Microsoft.AspNetCore.Mvc;
using NMShop.Data;
using NMShop.Shared.Models;
using System.Collections.Generic;
using System.Linq;

namespace NMShop.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        [HttpGet("{category}")]
        public ActionResult<IEnumerable<Product>> GetProductsByCategory(string category)
        {
            var products = TestDataProvider.GetTestProducts()
                .Where(p => p.ProductType == category.ToLower()).ToList();
            return Ok(products);
        }
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAllProducts()
        {
            var products = TestDataProvider.GetTestProducts();
            return Ok(products);
        }
    }
}