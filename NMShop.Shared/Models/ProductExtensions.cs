namespace NMShop.Shared.Models;
using System;
using System.Linq;
using System.Collections.Generic;

public static class ProductExtensions
{
    // Метод для фильтрации продуктов
    public static IEnumerable<Product> FilterProducts(this IEnumerable<Product> products, ProductFilter filter)
    {
        // Применяем фильтры
        if (!string.IsNullOrEmpty(filter.Brand))
        {
            products = products.Where(p => p.Brand.Equals(filter.Brand, StringComparison.OrdinalIgnoreCase));
        }

        if (filter.MinPrice.HasValue)
        {
            products = products.Where(p => p.PriceInfos.Any(pi => pi.Price >= filter.MinPrice.Value));
        }

        if (filter.MaxPrice.HasValue)
        {
            products = products.Where(p => p.PriceInfos.Any(pi => pi.Price <= filter.MaxPrice.Value));
        }

        if (!string.IsNullOrEmpty(filter.Gender))
        {
            products = products.Where(p => p.Gender.Equals(filter.Gender, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrEmpty(filter.Category))
        {
            products = products.Where(p => p.ProductType.Equals(filter.Category, StringComparison.OrdinalIgnoreCase));
        }

        if (filter.InStock)
        {
            products = products.Where(p => p.PriceInfos.Any(pi => pi.Stock > 0));
        }

        return products;
    }
}
