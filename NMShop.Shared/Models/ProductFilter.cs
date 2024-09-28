namespace NMShop.Shared.Models;


    public class ProductFilter
    {
        public string Brand { get; set; } // Бренд продукта
        public decimal? MinPrice { get; set; } // Минимальная цена
        public decimal? MaxPrice { get; set; } // Максимальная цена
        public string Gender { get; set; } // Пол (мужской, женский, унисекс)
        public string Category { get; set; } // Категория продукта
        public bool InStock { get; set; } // Товар в наличии или нет
    }

