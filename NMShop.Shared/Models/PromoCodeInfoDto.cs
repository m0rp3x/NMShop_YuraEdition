namespace NMShop.Shared.Models;

public class PromoCodeInfoDto
{
    public string Code { get; set; } // Название промокода
    public int DiscountPercent { get; set; } // Процент скидки
    public DateOnly? ExpirationDate { get; set; } // Срок действия
}
