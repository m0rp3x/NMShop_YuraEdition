using System.Runtime.CompilerServices;

namespace NMShop.Client.Data
{
    public static class Extensions
    {
        public static string ToString(this ProductType type)
        {
            return type switch
            {
                ProductType.Clothing => "clothing",
                ProductType.Shoe => "shoe",
                ProductType.Accessory => "accessory",
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

    public enum ProductType
    {
        Shoe,
        Clothing,
        Accessory
    }

    public enum Gender
    {
        Male, // Мужское
        Female, // Женское
        Unisex, // Унисекс
        Kids // Детское
    }
    public enum ClothingSize
    {
        XS = 1,
        S = 2,
        M = 3,
        L = 4,
        XL = 5,
        XXL = 6
    }

    public class Product
    {
        public int Id { get; set; } // Уникальный идентификатор товара
        public string Name { get; set; } // Название товара
        public string Brand { get; set; } // Бренд товара
        public string Article { get; set; } // Артикул
        public string Description { get; set; } // Описание товара
        public string ImageUrl { get; set; } = "/bebra.jpg"; // Изображение товара
        public Gender Gender { get; set; } // Пол: мужское, женское, унисекс, детское
        public string SubCategory { get; set; } // Подкатегория товара (например, спортивная обувь, верхняя одежда)
        public ProductType ProductType { get; set; } // Тип товара: обувь, одежда, аксессуары
        public Dictionary<string, string> Color { get; set; } // Цвет: название-значение (например, "Красный" => "#FF0000")
        public List<PriceInfo> PriceInfos { get; set; } // Список информации о цене и размере

        public Product()
        {
            Color = new Dictionary<string, string>();
            PriceInfos = new List<PriceInfo>();
        }
    }

    public class PriceInfo
    {
        public decimal Size { get; set; } // Размер в числовом формате
        public decimal Price { get; set; } // Цена за данный размер
        public decimal? DiscountPrice { get; set; } // Акционная цена (если есть)
        public int Stock { get; set; } // Количество товара на складе
    }
}
