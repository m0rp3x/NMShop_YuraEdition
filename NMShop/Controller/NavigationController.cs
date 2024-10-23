using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NMShop.Shared.Models;
using NMShop.Shared.Scaffold;
namespace NMShop.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NavigationController : ControllerBase
    {
        private readonly NMShopContext _context;

        public NavigationController(NMShopContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetAllNavigationUnits")]
        public async Task<ActionResult<List<NavigationUnit>>> GetAllNavigationUnits()
        {
            var categories = _context.NavigationItems
                .Where(item => item.ParentItemId == null)
                .ToList();

            var navigationUnits = categories.Select(category => new NavigationUnit
            {
                Category = new NavigationItem
                {
                    Id = category.Id,
                    Name = category.Name,
                    Link = category.Link,
                    ParentItemId = category.ParentItemId
                },
                Items = _context.NavigationItems
                    .Where(item => item.ParentItemId == category.Id)
                    .Select(item => new NavigationItem
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Link = item.Link,
                        ParentItemId = item.ParentItemId
                    }).ToList()
            }).ToList();

            return Ok(navigationUnits);
        }

        [HttpGet]
        [Route("GetAllBrandGalleries")]
        public async Task<ActionResult<List<BrandGallery>>> GetAllBrandGalleries()
        {
            var brandGalleries = await _context.BrandGalleries
                .Include(bg => bg.Brand)
                .Select(bg => new BrandGallery
                {
                    Id = bg.Id,
                    BrandId = bg.BrandId,
                    Image = bg.Image,
                    Brand = new Brand
                    {
                        Id = bg.Brand.Id,
                        Name = bg.Brand.Name
                    }
                })
                .ToListAsync();

            return Ok(brandGalleries);
        }

    }
}
