namespace NMShop.Shared.Models;

public class ProductDto
{
    public int Id { get; set; } // Уникальный идентификатор товара
    public string Name { get; set; } // Название товара
    public string Brand { get; set; } // Бренд товара
    public string Article { get; set; } // Артикул
    public string Description { get; set; } // Описание товара
    public List<ProductImage> Images { get; set; } // Изображения товара
    public string Gender { get; set; }  // Пол: мужское, женское, унисекс, детское
    public string SubCategory { get; set; } // Подкатегория товара (например, спортивная обувь, верхняя одежда)
    public string SelCategory { get; set; } //Категория продаж (например, новые релизы,хиты продаж)
    public string ProductType { get; set; } // Тип товара: обувь, одежда, аксессуары
    public DateOnly ReleaseDate { get; set; }
    public Dictionary<string, string> Color { get; set; } // Цвет: название-значение (например, "Красный" => "#FF0000")
    public List<PriceInfo> PriceInfos { get; set; } // Список информации о цене и размере

    public ProductDto()
    {
        Color = new Dictionary<string, string>();
        PriceInfos = new List<PriceInfo>();
        Images = new List<ProductImage>() { new() { IsMain = true} };
        Gender = "unisex";
    }
}
    
public class PriceInfo
{
    public decimal Size { get; set; } // Размер в числовом формате
    public decimal Price { get; set; } // Цена за данный размер
    public decimal? DiscountPrice { get; set; } // Акционная цена (если есть)
    public int Stock { get; set; } // Количество товара на складе

    public bool isHovered = false;
}

public class ProductImage
{
    public byte[] Bytes { get; set; }
    public bool IsMain { get; set; }
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
