using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NMShop.Scaffold;

namespace NMShop.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductAttributesController : ControllerBase
    {
        private readonly NMShopContext _context;

        public ProductAttributesController(NMShopContext context)
        {
            _context = context;
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<string>>> GetBrands()
        {
            var brands = await _context.Brands
                .Select(b => b.Name)
                .ToListAsync();

            return Ok(brands);
        }

        [HttpGet("product-types")]
        public async Task<ActionResult<IEnumerable<string>>> GetProductTypes()
        {
            var productTypes = await _context.ProductTypes
                .Select(pt => pt.Name)
                .ToListAsync();

            return Ok(productTypes);
        }

        [HttpGet("selling-categories")]
        public async Task<ActionResult<IEnumerable<string>>> GetSellingCategories()
        {
            var categories = await _context.SellingCategories
                .Select(sc => sc.Name)
                .ToListAsync();

            return Ok(categories);
        }

        [HttpGet("genders")]
        public async Task<ActionResult<IEnumerable<string>>> GetGenders()
        {
            var genders = await _context.Genders
                .Select(g => g.Name)
                .ToListAsync();

            return Ok(genders);
        }
    }
}