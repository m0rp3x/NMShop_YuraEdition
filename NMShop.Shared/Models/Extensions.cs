using System.Web;

namespace NMShop.Shared.Models
{
    public static class Extensions
    {
        public static string ToQueryString(this ProductFilter filter)
        {
            var queryParams = new Dictionary<string, string>();

            if (filter.Brands != null && filter.Brands.Any()) queryParams["Brands"] = string.Join(",", filter.Brands);
            if (filter.MinPrice.HasValue) queryParams["MinPrice"] = filter.MinPrice.Value.ToString();
            if (filter.MaxPrice.HasValue) queryParams["MaxPrice"] = filter.MaxPrice.Value.ToString();
            if (!string.IsNullOrEmpty(filter.Gender)) queryParams["Gender"] = filter.Gender;
            if (!string.IsNullOrEmpty(filter.Category)) queryParams["Category"] = filter.Category;
            if (filter.InStock) queryParams["InStock"] = "true";
            if (!string.IsNullOrEmpty(filter.Color)) queryParams["Color"] = filter.Color;
            if (filter.SubCategories != null && filter.SubCategories.Any()) queryParams["SubCategories"] = string.Join(",", filter.SubCategories);
            if (!string.IsNullOrEmpty(filter.SelCategory)) queryParams["SelCategory"] = filter.SelCategory;
            if (!string.IsNullOrEmpty(filter.SortBy)) queryParams["SortBy"] = filter.SortBy;
            if (filter.IsAscending) queryParams["IsAscending"] = "true";
            if (filter.Skip.HasValue) queryParams["Skip"] = filter.Skip.Value.ToString();
            if (filter.Take.HasValue) queryParams["Take"] = filter.Take.Value.ToString();
            if (filter.MinSize.HasValue) queryParams["MinSize"] = filter.MinSize.Value.ToString();
            if (filter.MaxSize.HasValue) queryParams["MaxSize"] = filter.MaxSize.Value.ToString();

            return string.Join("&", queryParams.Select(kv => $"{kv.Key}={HttpUtility.UrlEncode(kv.Value)}"));
        }

        public static string ToSrcString(this byte[]? imageBytes)
        {
            if (imageBytes is not null && imageBytes.Count() != 0)
            {
                return "data:image/jpeg;base64," + Convert.ToBase64String(imageBytes);
            }
            else
            {
                return $"assets/{GetPlaceHolderName()}.jpg";
            }
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
            else
            {
                return $"assets/{GetPlaceHolderName()}.jpg";
            }
        }

        private static readonly Random random = new Random();

        public static string GetPlaceHolderName()
        {
            string[] strings = { "placeholder", "bebra", "biba", "boba" };
            int index = random.Next(strings.Length);
            return strings[index];
        }

        public static string ToPreFormatedString(this decimal? me) => Math.Abs(me ?? 0).ToString("# ### ### ### ###");
        public static string ToPreFormatedString(this decimal me) => me.ToString("# ### ### ### ###");

        public static (decimal MinPrice, decimal? MinDiscountPrice) GetMinPriceAndDiscount(this ProductDto product)
        {
            if (product.PriceInfos == null || !product.PriceInfos.Any())
            {
                throw new InvalidOperationException("Нет доступных цен для продукта.");
            }

            // Минимальная цена
            var minPrice = product.PriceInfos.Min(p => p.Price);

            // Минимальная цена со скидкой (если есть)
            var minDiscountPrice = product.PriceInfos
                .Where(p => p.DiscountPrice.HasValue)
                .Min(p => p.DiscountPrice);

            return (minPrice, minDiscountPrice);
        }

        public static TEnum ToEnum<TEnum>(this string value) where TEnum : struct
        {
            if (Enum.TryParse(value, true, out TEnum result))
            {
                return result;
            }
            throw new ArgumentException($"Unable to convert '{value}' to {typeof(TEnum).Name}");
        }
    }

}
