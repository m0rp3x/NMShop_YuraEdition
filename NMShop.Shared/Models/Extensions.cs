using System.Web;

namespace NMShop.Shared.Models
{
    public static class Extensions
    {
        public static string ToQueryString(this ProductFilter filter)
        {
            var queryParams = new Dictionary<string, string>();

            if (!string.IsNullOrWhiteSpace(filter.SearchQuery)) queryParams["SearchQuery"] = string.Join(",", filter.SearchQuery);
            if (filter.BrandIds != null && filter.BrandIds.Any()) queryParams["BrandIds"] = string.Join(",", filter.BrandIds);
            if (filter.MinPrice.HasValue) queryParams["MinPrice"] = filter.MinPrice.Value.ToString();
            if (filter.MaxPrice.HasValue) queryParams["MaxPrice"] = filter.MaxPrice.Value.ToString();
            if (filter.GenderIds != null && filter.GenderIds.Any()) queryParams["GenderIds"] = string.Join(",", filter.GenderIds);
            if (filter.CategoryId.HasValue) queryParams["CategoryId"] = filter.CategoryId.Value.ToString();
            if (filter.InStock) queryParams["InStock"] = "true";
            if (filter.ColorIds != null && filter.ColorIds.Any()) queryParams["ColorIds"] = string.Join(",", filter.ColorIds);
            if (filter.SubCategoryIds != null && filter.SubCategoryIds.Any()) queryParams["SubCategoryIds"] = string.Join(",", filter.SubCategoryIds);
            if (filter.SelCategoryIds != null && filter.SelCategoryIds.Any()) queryParams["SelCategoryIds"] = string.Join(",", filter.SelCategoryIds);
            if (!string.IsNullOrEmpty(filter.SortBy)) queryParams["SortBy"] = filter.SortBy;
            if (filter.IsAscending) queryParams["IsAscending"] = "true";
            if (filter.Skip.HasValue) queryParams["Skip"] = filter.Skip.Value.ToString();
            if (filter.Take.HasValue) queryParams["Take"] = filter.Take.Value.ToString();
            if (filter.MinSize.HasValue) queryParams["MinSize"] = filter.MinSize.Value.ToString();
            if (filter.MaxSize.HasValue) queryParams["MaxSize"] = filter.MaxSize.Value.ToString();

            return string.Join("&", queryParams.Select(kv => $"{kv.Key}={HttpUtility.UrlEncode(kv.Value)}"));
        }

        static string _placeHolder = "assets/biba.jpg";

        public static string ToSrcString(this byte[]? imageBytes)
        {
            if (imageBytes is not null && imageBytes.Count() != 0)
            {
                return "data:image/jpeg;base64," + Convert.ToBase64String(imageBytes);
            }
            else return _placeHolder;
        }

        public static string GetMainImageSrc(this List<ProductImage> productImages)
        {
            if (productImages is not null && productImages.Count() != 0)
            {
                if (productImages.Where(i => i.IsMain).Count() != 1)
                {
                    return productImages.First().Bytes.ToSrcString();
                }
                return productImages.Single(pi => pi.IsMain).Bytes.ToSrcString();
            }
            else return _placeHolder;
        }

        public static string ToPreFormatedString(this decimal? me) => Math.Abs(me ?? 0).ToString("# ### ### ### ###");
        public static string ToPreFormatedString(this decimal me) => me.ToString("# ### ### ### ###");

        public static (decimal MinPrice, decimal? MinDiscountPrice) GetMinPriceAndDiscount(this ProductDto product)
        {
            if (product.PriceInfos == null || !product.PriceInfos.Any())
            {
                Console.Error.WriteLine($"Не найдено записей цены товара {product.Name}, артикул {product.Article}");
                return (0, null);
            }
            
            
            var validPrices = product.PriceInfos.Where(p => p.Price > 0);
            var minPrice = validPrices.Any() ? validPrices.Min(p => p.Price) : 0;

            
            var validDiscountPrices = validPrices.Where(p => p.DiscountPrice.HasValue && p.DiscountPrice > 0);
            var minDiscountPrice = validDiscountPrices.Any() ? validDiscountPrices.Min(p => p.DiscountPrice) : null;

            return (minPrice, minDiscountPrice);
        }

        public static TEnum ToEnum<TEnum>(this string value) where TEnum : struct
        {
            if (Enum.TryParse(value, true, out TEnum result))
            {
                return result;
            }
            throw new ArgumentNullException($"Unable to convert '{value}' to {typeof(TEnum).Name}");
        }
    }
}
