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
        public async Task<ActionResult<IEnumerable<string>>> GetProductTypes([FromQuery] string parentCategory = null)
        {
            if (!string.IsNullOrEmpty(parentCategory))
            {
                var parentType = await _context.ProductTypes
                    .Include(pt => pt.InverseParentType)
                    .FirstOrDefaultAsync(pt => EF.Functions.ILike(pt.Name, parentCategory));

                if (parentType == null)
                {
                    return NotFound("Parent category not found.");
                }

                var subCategories = parentType.InverseParentType
                    .Select(pt => pt.Name)
                    .ToList();

                return Ok(subCategories);
            }
            else
            {
                var subCategories = await _context.ProductTypes
                    .Where(pt => pt.ParentTypeId != null)
                    .Select(pt => pt.Name)
                    .ToListAsync();

                return Ok(subCategories);
            }
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