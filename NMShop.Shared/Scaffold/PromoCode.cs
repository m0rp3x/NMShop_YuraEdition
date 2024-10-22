using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NMShop.Shared.Scaffold;

[Table("PromoCodes", Schema = "NMShop")]
public partial class PromoCode
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [DisplayName("Идентификатор промокода")]
    public int Id { get; set; }

    [StringLength(50)]
    [DisplayName("Код промокода")]
    public string Code { get; set; } = null!;

    [DisplayName("Максимальное количество использований")]
    public int MaxUsages { get; set; }

    [DisplayName("Процент скидки")]
    public int DiscountPercent { get; set; }

    [DisplayName("Дата истечения срока действия")]
    public DateOnly? ExpirationDate { get; set; }

    [InverseProperty("PromoCode")]
    [Display(AutoGenerateField = false)]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public override string ToString()
    {
        return $"Промокод: {Code}, Скидка: {DiscountPercent}%, Действителен до: {(ExpirationDate.HasValue ? ExpirationDate.Value.ToString("yyyy-MM-dd") : "не ограничен")}";
    }
}
