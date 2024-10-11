using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using NMShop.Scaffold;
using NMShop.Shared.Models;
using System.Linq;

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
        //            .ThenInclude(pt => pt.ParentType)
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
                    .ThenInclude(pt => pt.ParentType)
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

        [HttpGet("product-by-article")]
        public async Task<ActionResult<ProductDto>> GetProductByArticle([FromQuery] string article)
        {
            if (string.IsNullOrEmpty(article))
            {
                return BadRequest("Article is required.");
            }

            Console.Clear();
            Console.WriteLine(article);

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
        //            .ThenInclude(pt => pt.ParentType)
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
            try
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

                // Фильтрация по категории
                if (!string.IsNullOrEmpty(filter.Category))
                {
                    var productType = await _context.ProductTypes.Include(pt => pt.InverseParentType)
                        .FirstOrDefaultAsync(pt => EF.Functions.ILike(pt.Name, filter.Category));

                    if (productType != null)
                    {
                        if (productType.InverseParentType.Any())
                        {
                            // Если указанный тип является родительским, получаем продукты дочерних типов
                            var childTypeIds = productType.InverseParentType.Select(pt => pt.Id).ToList();
                            productsQuery = productsQuery.Where(p => childTypeIds.Contains(p.ProductTypeId));
                        }
                        else
                        {
                            // Если указанный тип является дочерним, получаем продукты этого типа
                            productsQuery = productsQuery.Where(p => p.ProductTypeId == productType.Id);
                        }
                    }
                }

                // Фильтрация по подкатегориям
                if (filter.SubCategories != null && filter.SubCategories.Any())
                {
                    productsQuery = productsQuery.Where(p => filter.SubCategories.Contains(p.ProductType.Name));
                }

                // Фильтрация по брендам
                if (filter.Brands != null && filter.Brands.Any())
                {
                    productsQuery = productsQuery.Where(p => filter.Brands.Contains(p.Brand.Name));
                }

                // Фильтрация по рейтингу продаж
                if (!string.IsNullOrEmpty(filter.SelCategory))
                {
                    productsQuery = productsQuery.Where(p => EF.Functions.ILike(p.SellingCategory.Name, filter.SelCategory));
                }

                // Фильтрация по полу
                if (!string.IsNullOrEmpty(filter.Gender))
                {
                    productsQuery = productsQuery.Where(p => EF.Functions.ILike(p.Gender.Name, filter.Gender));
                }

                // Фильтрация по минимальной цене
                if (filter.MinPrice.HasValue)
                {
                    productsQuery = productsQuery.Where(p => p.StockInfos.Any(si => si.Price >= filter.MinPrice.Value));
                }

                // Фильтрация по максимальной цене
                if (filter.MaxPrice.HasValue)
                {
                    productsQuery = productsQuery.Where(p => p.StockInfos.Any(si => si.Price <= filter.MaxPrice.Value));
                }

                // Фильтрация по цвету
                if (!string.IsNullOrEmpty(filter.Color))
                {
                    productsQuery = productsQuery.Where(p => EF.Functions.ILike(p.Color.Name, filter.Color));
                }

                // Фильтрация по наличию на складе
                if (filter.InStock)
                {
                    productsQuery = productsQuery.Where(p => p.StockInfos.Any(si => si.AmountInStock > 0));
                }

                // **Фильтрация по размерам**
                if (filter.Sizes != null && filter.Sizes.Any())
                {
                    productsQuery = productsQuery.Where(p => p.StockInfos.Any(si => filter.Sizes.Contains(si.Size)));
                }

                // Фильтрация по поисковому запросу
                if (!string.IsNullOrEmpty(filter.SearchQuery))
                {
                    productsQuery = productsQuery.Where(p => EF.Functions.ILike(p.Name, $"%{filter.SearchQuery}%"));
                }

                // Сортировка
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
                        default:
                            productsQuery = filter.IsAscending
                                ? productsQuery.OrderBy(p => p.OrderParts.Sum(op => op.Amount))
                                : productsQuery.OrderByDescending(p => p.OrderParts.Sum(op => op.Amount));
                            break;
                    }
                }

                // Пропуск и ограничение количества записей
                if (filter.Skip > 0)
                {
                    productsQuery = productsQuery.Skip(filter.Skip.Value);
                }

                if (filter.Take > 0)
                {
                    productsQuery = productsQuery.Take(filter.Take.Value);
                }

                var products = await productsQuery.ToListAsync();
                return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}