namespace NMShop.Shared.Models;

public static class Extensions
{
    public static string Localize(this string type)
    {
        return type.ToLower() switch
        {
            "clothes" => "Одежда",
            "shoes" => "Обувь",
            "accessories" => "Аксессуары",
            _ => "error invalid ProductType"
        };
    }

    public static string ToSrcString(this byte[]? imageBytes)
    {
        if (imageBytes is not null && imageBytes.Count() != 0)
        {
            return "data:image/jpeg;base64," + Convert.ToBase64String(imageBytes);
        }
        else
        {
            return $"assets/{GetRandomString()}.jpg";
        }
    }

    public static string GetMainImageSrc(this List<ProductImage> productImages)
    {
        if (productImages is not null && productImages.Count() != 0)
        { 
            return productImages.Single(pi => pi.IsMain).Bytes.ToSrcString();
        }
        else
        {
            return $"assets/{GetRandomString()}.jpg";
        }
    }

    private static readonly Random random = new Random();

    public static string GetRandomString()
    {
        string[] strings = { "placeholder", "bebra", "biba", "boba" };
        int index = random.Next(strings.Length);
        return strings[index];
    }

    public static string ToPreFormatedString(this decimal? me) => Math.Abs(me ?? 0).ToString("# ### ### ### ###");
    public static string ToPreFormatedString(this decimal me) => me.ToString("# ### ### ### ###");

    public static (decimal MinPrice, decimal? MinDiscountPrice) GetMinPriceAndDiscount(this Product product)
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
