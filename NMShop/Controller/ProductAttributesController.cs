using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NMShop.Shared.Scaffold;
namespace NMShop.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductAttributesController : ControllerBase
    {
        private readonly HttpClient _http;
        private readonly NMShopContext _context;

        public ProductAttributesController(NMShopContext context, HttpClient http)
        {
            _context = context;
            _http = http;
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<Brand>>> GetBrands()
        {
            var brands = await _context.Brands
                .Select(b => new { b.Id, b.Name })
                .ToListAsync();

            return Ok(brands);
        }

        [HttpGet("product-types")]
        public async Task<ActionResult<IEnumerable<object>>> GetProductTypes([FromQuery] int? parentCategoryId = null)
        {
            if (parentCategoryId.HasValue)
            {
                var parentType = await _context.ProductTypes
                    .Include(pt => pt.InverseParentType)
                    .FirstOrDefaultAsync(pt => pt.Id == parentCategoryId);

                if (parentType == null)
                {
                    return NotFound("Parent category not found.");
                }

                var subCategories = parentType.InverseParentType
                    .Select(pt => new { pt.Id, pt.Name })
                    .ToList();

                return Ok(subCategories);
            }
            else
            {
                var subCategories = await _context.ProductTypes
                    .Where(pt => pt.ParentTypeId != null)
                    .Select(pt => new { pt.Id, pt.Name })
                    .ToListAsync();

                return Ok(subCategories);
            }
        }

        [HttpGet("category-size-display-type")]
        public async Task<ActionResult<string>> GetCategorySizeDisplayType([FromQuery] int categoryId)
        {
            var productType = await _context.ProductTypes
                .Include(pt => pt.ParentType)
                .FirstOrDefaultAsync(pt => pt.Id == categoryId);

            if (productType == null)
            {
                return NotFound("Category not found.");
            }

            string sizeDisplayType = productType.ParentType?.SizeDisplayType ?? productType.SizeDisplayType ?? "none";

            return Ok(sizeDisplayType);
        }

        [HttpGet("selling-categories")]
        public async Task<ActionResult<IEnumerable<SellingCategory>>> GetSellingCategories()
        {
            var categories = await _context.SellingCategories
                .Select(sc => new { sc.Id, sc.Name })
                .ToListAsync();

            return Ok(categories);
        }

        [HttpGet("genders")]
        public async Task<ActionResult<IEnumerable<Gender>>> GetGenders()
        {
            var genders = await _context.Genders
                .Select(g => new { g.Id, g.Name })
                .ToListAsync();

            return Ok(genders);
        }

        [HttpGet("colors")]
        public async Task<ActionResult<IEnumerable<ProductColor>>> GetColors()
        {
            var colors = await _context.ProductColors
                .Select(c => new { c.Id, c.Name, c.Value })
                .ToListAsync();
            return Ok(colors);
        }

        [HttpGet("category-id-by-name")]
        public async Task<ActionResult<int?>> GetCategoryIdByName([FromQuery] string categoryName)
        {
            if (string.IsNullOrEmpty(categoryName))
            {
                return BadRequest("Category name is required.");
            }

            var category = await _context.ProductTypes
                .Where(pt => EF.Functions.ILike(pt.Name, categoryName))
                .Select(pt => pt.Id)
                .FirstOrDefaultAsync();

            if (category == 0)
            {
                return NotFound("Category not found.");
            }

            return Ok(category);
        }
        [HttpGet("brand-id-by-name")]
        public async Task<ActionResult<int?>> GetBrandIdByName([FromQuery] string brandName)
        {
            if (string.IsNullOrEmpty(brandName))
            {
                return BadRequest("Brand name cannot be null or empty.");
            }

            var brandId = await _context.Brands
                .Where(b => EF.Functions.ILike(b.Name, brandName))
                .Select(b => b.Id)
                .FirstOrDefaultAsync();

            if (brandId == 0)
            {
                return NotFound("Brand not found.");
            }

            return Ok(brandId);
        }





    }
}