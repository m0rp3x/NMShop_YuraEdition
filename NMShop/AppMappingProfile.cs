using AutoMapper;
using NMShop.Shared.Scaffold;
using NMShop.Shared.Models;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand.Name))
            .ForMember(dest => dest.Color, opt => opt.MapFrom(src => new Dictionary<string, string> { { src.Color.Name, src.Color.Value } }))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.Name))
            .ForMember(dest => dest.SizeDisplayType, opt => opt.MapFrom(src => (src.ProductType.ParentType == null ? (src.ProductType.SizeDisplayType ?? "none") : src.ProductType.ParentType.SizeDisplayType)))
            .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => (src.ProductType.ParentType == null ? src.ProductType.Name : src.ProductType.ParentType.Name)))
            .ForMember(dest => dest.SubCategory, opt => opt.MapFrom(src => src.ProductType.Name))
            .ForMember(dest => dest.SelCategory, opt => opt.MapFrom(src => src.SellingCategory.Name))
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.ProductImages.Select(i => new NMShop.Shared.Models.ProductImage { Bytes = i.Bytes, IsMain = i.IsMain }).ToList()))
            .ForMember(dest => dest.PriceInfos, opt => opt.MapFrom(src => src.StockInfos.Select(si => new PriceInfo
            {
                Size = si.Size,
                Price = si.Price,
                DiscountPrice = si.DiscountPrice,
                Stock = si.AmountInStock
            }).ToList()));
    }
}