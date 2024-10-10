using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using NMShop.Scaffold;
using NMShop.Shared.Models;

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

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
        //{
        //    var products = await _context.Products
        //        .Include(p => p.Brand)
        //        .Include(p => p.Color)
        //        .Include(p => p.Gender)
        //        .Include(p => p.ProductType)
        //        .Include(p => p.SellingCategory)
        //        .Include(p => p.ProductImages)
        //        .Include(p => p.StockInfos)
        //        .ToListAsync();

        //    return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
        //}

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var product = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Color)
                .Include(p => p.Gender)
                .Include(p => p.ProductType)
                .Include(p => p.SellingCategory)
                .Include(p => p.ProductImages)
                .Include(p => p.StockInfos)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ProductDto>(product));
        }

        //[HttpGet("category/{category}")]
        //public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByCategory(string category)
        //{
        //    var productType = await _context.ProductTypes.FirstOrDefaultAsync(pt => pt.Name.Equals(category));

        //    if (productType == null)
        //    {
        //        return NotFound();
        //    }

        //    var products = await _context.Products
        //        .Include(p => p.Brand)
        //        .Include(p => p.Color)
        //        .Include(p => p.Gender)
        //        .Include(p => p.ProductType)
        //        .Include(p => p.SellingCategory)
        //        .Include(p => p.ProductImages)
        //        .Include(p => p.StockInfos)
        //        .Where(p => p.ProductTypeId == productType.Id)
        //        .ToListAsync();

        //    return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
        //}

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetFilteredProducts([FromQuery] ProductFilter filter)
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
                .Include(p => p.SellingCategory)
                .Include(p => p.ProductImages)
                .Include(p => p.StockInfos)
                .AsQueryable();

            if (!string.IsNullOrEmpty(filter.Category))
            {
                var productType = await _context.ProductTypes.Include(pt => pt.InverseParentType)
                    .FirstOrDefaultAsync(pt => EF.Functions.ILike(pt.Name, filter.Category));

                if (productType != null)
                {
                    if (productType.InverseParentType.Any())
                    {
                        // If the specified type is a parent, get products of child types
                        var childTypeIds = productType.InverseParentType.Select(pt => pt.Id).ToList();
                        productsQuery = productsQuery.Where(p => childTypeIds.Contains(p.ProductTypeId));
                    }
                    else
                    {
                        // If the specified type is a child, get products of that type
                        productsQuery = productsQuery.Where(p => p.ProductTypeId == productType.Id);
                    }
                }
            }

            if (filter.SubCategories != null && filter.SubCategories.Any())
            {
                productsQuery = productsQuery.Where(p => filter.SubCategories.Contains(p.ProductType.Name));
            }

            if (filter.Brands != null && filter.Brands.Any())
            {
                productsQuery = productsQuery.Where(p => filter.Brands.Contains(p.Brand.Name));
            }

            if (!string.IsNullOrEmpty(filter.SelCategory))
            {
                productsQuery = productsQuery.Where(p => EF.Functions.ILike(p.SellingCategory.Name, filter.SelCategory));
            }

            if (!string.IsNullOrEmpty(filter.Gender))
            {
                productsQuery = productsQuery.Where(p => EF.Functions.ILike(p.Gender.Name, filter.Gender));
            }

            if (filter.MinPrice.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.StockInfos.Any(si => si.Price >= filter.MinPrice.Value));
            }

            if (filter.MaxPrice.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.StockInfos.Any(si => si.Price <= filter.MaxPrice.Value));
            }

            if (!string.IsNullOrEmpty(filter.Color))
            {
                productsQuery = productsQuery.Where(p => EF.Functions.ILike(p.Color.Name, filter.Color));
            }

            if (filter.InStock)
            {
                productsQuery = productsQuery.Where(p => p.StockInfos.Any(si => si.AmountInStock > 0));
            }

            if (!string.IsNullOrEmpty(filter.SortBy))
            {
                switch (filter.SortBy.ToLower())
                {
                    case "price":
                        productsQuery = filter.IsAscending
                            ? productsQuery.OrderBy(p => p.StockInfos.FirstOrDefault().Price)
                            : productsQuery.OrderByDescending(p => p.StockInfos.FirstOrDefault().Price);
                        break;
                    case "newest":
                        productsQuery = filter.IsAscending
                            ? productsQuery.OrderBy(p => p.DateAdded)
                            : productsQuery.OrderByDescending(p => p.DateAdded);
                        break;
                    case "popularity":
                    default:
                        productsQuery = filter.IsAscending
                            ? productsQuery.OrderBy(p => p.OrderParts.Sum(op => op.Amount))
                            : productsQuery.OrderByDescending(p => p.OrderParts.Sum(op => op.Amount));
                        break;
                }
            }

            if (filter.Skip.HasValue)
            {
                productsQuery = productsQuery.Skip(filter.Skip.Value);
            }

            if (filter.Take.HasValue)
            {
                productsQuery = productsQuery.Take(filter.Take.Value);
            }

            var products = await productsQuery.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
        }

    }
}