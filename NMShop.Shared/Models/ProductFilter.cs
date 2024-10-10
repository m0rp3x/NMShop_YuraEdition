namespace NMShop.Shared.Models;


public class ProductFilter
{
    public List<string> Brands { get; set; } = new List<string>(); // Бренды продукта
    public decimal? MinPrice { get; set; } // Минимальная цена
    public decimal? MaxPrice { get; set; } // Максимальная цена
    public string? Gender { get; set; } // Пол (мужской, женский, унисекс)
    public string? Category { get; set; } // Категория продукта
    public bool InStock { get; set; } // Товар в наличии или нет
    public string? Color { get; set; } // Цвет продукта
    public List<string> SubCategories { get; set; } = new List<string>(); // Список дочерних категорий
    public string? SelCategory { get; set; }
    public string? SortBy { get; set; } // Поле для сортировки
    public bool IsAscending { get; set; }  // Направление сортировки (asc/desc)
    public int? Skip { get; set; } // Сколько пропустить
    public int? Take { get; set; } // Сколько взять
    public int? MinSize { get; set; } // Минимальный размер
    public int? MaxSize { get; set; } // Максимальный размер
}