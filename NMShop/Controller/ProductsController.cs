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
        
        [HttpGet("filter")]
        public ActionResult<IEnumerable<Product>> GetFilteredProducts([FromQuery] ProductFilter filter)
        {
            var products = TestDataProvider.GetTestProducts();

            // Фильтр по бренду
            if (!string.IsNullOrEmpty(filter.Brand))
            {
                products = products.Where(p => p.Brand.Equals(filter.Brand, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(filter.SubCategory))
            {
                products = products.Where(p => p.SubCategory.Equals(filter.SubCategory, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // Фильтр по категории (ProductType)
            if (!string.IsNullOrEmpty(filter.Category))
            {
                products = products.Where(p => p.ProductType.Equals(filter.Category, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // Фильтр по полу (Gender)
            if (!string.IsNullOrEmpty(filter.Gender))
            {
                products = products.Where(p => p.Gender.Equals(filter.Gender, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // Фильтр по минимальной цене
            if (filter.MinPrice.HasValue)
            {
                products = products.Where(p => p.PriceInfos.Any(pi => pi.Price >= filter.MinPrice.Value)).ToList();
            }

            // Фильтр по максимальной цене
            if (filter.MaxPrice.HasValue)
            {
                products = products.Where(p => p.PriceInfos.Any(pi => pi.Price <= filter.MaxPrice.Value)).ToList();
            }

            // Фильтр по цвету
            if (!string.IsNullOrEmpty(filter.Color))
            {
                products = products.Where(p => p.Color.Keys.Any(c => c.Equals(filter.Color, StringComparison.OrdinalIgnoreCase))).ToList();
            }

            // Фильтр по наличию на складе (InStock)
            if (filter.InStock)
            {
                products = products.Where(p => p.PriceInfos.Any(pi => pi.Stock > 0)).ToList();
            }

            return Ok(products);
        }




    }
}