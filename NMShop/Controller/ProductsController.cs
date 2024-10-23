using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using NMShop.Shared.Scaffold;
using NMShop.Shared.Models;
using System.Linq;
using System.Diagnostics;

namespace NMShop.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly NMShopContext _context;
        private readonly IMapper _mapper;

        public ProductsController(NMShopContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //[HttpGet("{id}")]
        //public async Task<ActionResult<ProductDto>> GetProductById(int id)
        //{
        //    var product = await _context.Products
        //        .Include(p => p.Brand)
        //        .Include(p => p.Color)
        //        .Include(p => p.Gender)
        //        .Include(p => p.ProductType)
        //            .ThenInclude(pt => pt.ParentType)
        //        .Include(p => p.SellingCategory)
        //        .Include(p => p.ProductImages)
        //        .Include(p => p.StockInfos)
        //        .FirstOrDefaultAsync(p => p.Id == id);

        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(_mapper.Map<ProductDto>(product));
        //}

        [HttpGet("product-by-article")]
        public async Task<ActionResult<ProductDto>> GetProductByArticle([FromQuery] string article)
        {
            if (string.IsNullOrEmpty(article))
            {
                return BadRequest("Article is required.");
            }

            var product = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Color)
                .Include(p => p.Gender)
                .Include(p => p.ProductType)
                    .ThenInclude(pt => pt.ParentType)
                .Include(p => p.SellingCategory)
                .Include(p => p.StockInfos)
                .FirstOrDefaultAsync(p => EF.Functions.ILike(p.Article, article));

            if (product == null)
            {
                return NotFound("Product not found.");
            }

            return Ok(_mapper.Map<ProductDto>(product));
        }

        // Controller Method
        [HttpPost("filter")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetFilteredProducts([FromBody] ProductFilter filter)
        {
            if (filter.Take > 100)
            {
                return BadRequest("Cannot request more than 100 products at a time.");
            }

            var productsQuery = _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Color)
                .Include(p => p.Gender)
                .Include(p => p.ProductType)
                .ThenInclude(pt => pt.ParentType)
                .Include(p => p.SellingCategory)
                .Include(p => p.ProductImages)
                .Include(p => p.StockInfos)
                .AsQueryable();

            // Filter by Parent Category
            if (filter.CategoryId.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.ProductType.ParentTypeId == filter.CategoryId);
            }

            // Filter by Subcategories
            if (filter.SubCategoryIds != null && filter.SubCategoryIds.Any())
            {
                productsQuery = productsQuery.Where(p => filter.SubCategoryIds.Contains(p.ProductTypeId));
            }

            // Filter by Brands
            if (filter.BrandIds != null && filter.BrandIds.Any())
            {
                productsQuery = productsQuery.Where(p => filter.BrandIds.Contains(p.BrandId));
            }

            // Filter by Selling Category
            if (filter.SelCategoryIds != null && filter.SelCategoryIds.Any())
            {
                productsQuery = productsQuery.Where(p => filter.SelCategoryIds.Contains(p.SellingCategoryId));
            }

            // Filter by Gender
            if (filter.GenderIds != null && filter.GenderIds.Any())
            {
                productsQuery = productsQuery.Where(p => filter.GenderIds.Contains(p.GenderId));
            }

            // Filter by Minimum Price
            if (filter.MinPrice.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.StockInfos.Any(si => si.Price >= filter.MinPrice.Value));
            }

            // Filter by Maximum Price
            if (filter.MaxPrice.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.StockInfos.Any(si => si.Price <= filter.MaxPrice.Value));
            }

            // Filter by Minimum Size
            if (filter.MinSize.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.StockInfos.Any(si => si.Size >= filter.MinSize.Value));
            }

            // Filter by Maximum Size
            if (filter.MaxSize.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.StockInfos.Any(si => si.Size <= filter.MaxSize.Value));
            }

            // Filter by In Stock
            if (filter.InStock)
            {
                productsQuery = productsQuery.Where(p => p.StockInfos.Any(si => si.AmountInStock > 0));
            }

            // Filter by Search Query
            if (!string.IsNullOrEmpty(filter.SearchQuery))
            {
                productsQuery = productsQuery.Where(p => EF.Functions.ILike(p.Name, $"%{filter.SearchQuery}%"));
            }

            // Filter by Colors
            if (filter.ColorIds != null && filter.ColorIds.Any())
            {
                productsQuery = productsQuery.Where(p => filter.ColorIds.Contains(p.ColorId));
            }

            // Sorting
            if (!string.IsNullOrEmpty(filter.SortBy))
            {
                switch (filter.SortBy.ToLower())
                {
                    case "price":
                        productsQuery = filter.IsAscending
                            ? productsQuery.OrderBy(p => p.StockInfos.Min(si => si.Price))
                            : productsQuery.OrderByDescending(p => p.StockInfos.Max(si => si.Price));
                        break;
                    case "newest":
                        productsQuery = filter.IsAscending
                            ? productsQuery.OrderBy(p => p.DateAdded)
                            : productsQuery.OrderByDescending(p => p.DateAdded);
                        break;
                    case "popularity":
                        productsQuery = filter.IsAscending
                            ? productsQuery.OrderBy(p => p.OrderParts.Sum(op => op.Amount))
                            : productsQuery.OrderByDescending(p => p.OrderParts.Sum(op => op.Amount));
                        break;
                    default:
                        productsQuery = productsQuery.OrderBy(p => p.Id);
                        break;
                }
            }
            else
            {
                // Default sorting
                productsQuery = productsQuery.OrderBy(p => p.Id);
            }


            var guid = new Random().Next();


            // Skip and Take
            if (filter.Skip.HasValue && filter.Skip.Value > 0)
            {
                productsQuery = productsQuery.Skip(filter.Skip.Value);
            }
            if (filter.Take.HasValue && filter.Take.Value > 0)
            {
                productsQuery = productsQuery.Take(filter.Take.Value);
            }

            var products = await productsQuery.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
        }

        [HttpPost("filter-count")]
        public async Task<ActionResult<int>> GetFilteredProductsCount([FromBody] ProductFilter filter)
        {
            var productsQuery = _context.Products.AsQueryable();

            if (filter.CategoryId.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.ProductType.ParentTypeId == filter.CategoryId);
            }

            if (filter.SubCategoryIds != null && filter.SubCategoryIds.Any())
            {
                productsQuery = productsQuery.Where(p => filter.SubCategoryIds.Contains(p.ProductTypeId));
            }

            if (filter.BrandIds != null && filter.BrandIds.Any())
            {
                productsQuery = productsQuery.Where(p => filter.BrandIds.Contains(p.BrandId));
            }

            if (filter.SelCategoryIds != null && filter.SelCategoryIds.Any())
            {
                productsQuery = productsQuery.Where(p => filter.SelCategoryIds.Contains(p.SellingCategoryId));
            }

            if (filter.GenderIds != null && filter.GenderIds.Any())
            {
                productsQuery = productsQuery.Where(p => filter.GenderIds.Contains(p.GenderId));
            }

            if (filter.MinPrice.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.StockInfos.Any(si => si.Price >= filter.MinPrice.Value));
            }

            if (filter.MaxPrice.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.StockInfos.Any(si => si.Price <= filter.MaxPrice.Value));
            }

            if (filter.MinSize.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.StockInfos.Any(si => si.Size >= filter.MinSize.Value));
            }

            if (filter.MaxSize.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.StockInfos.Any(si => si.Size <= filter.MaxSize.Value));
            }

            if (filter.InStock)
            {
                productsQuery = productsQuery.Where(p => p.StockInfos.Any(si => si.AmountInStock > 0));
            }

            if (!string.IsNullOrEmpty(filter.SearchQuery))
            {
                productsQuery = productsQuery.Where(p => EF.Functions.ILike(p.Name, $"%{filter.SearchQuery}%"));
            }

            if (filter.ColorIds != null && filter.ColorIds.Any())
            {
                productsQuery = productsQuery.Where(p => filter.ColorIds.Contains(p.ColorId));
            }

            int count = await productsQuery.CountAsync();

            return Ok(count);
        }

    }
}