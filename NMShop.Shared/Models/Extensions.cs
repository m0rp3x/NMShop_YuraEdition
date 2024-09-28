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
}
