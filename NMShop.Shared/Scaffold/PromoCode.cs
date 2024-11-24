using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NMShop.Shared.Scaffold;

[Table("PromoCodes", Schema = "NMShop")]
[Index("Code", Name = "PromoCodes_Code_key", IsUnique = true)]
public partial class PromoCode
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Code { get; set; } = null!;

    public int MaxUsages { get; set; }

    public int DiscountPercent { get; set; }

    public DateOnly? ExpirationDate { get; set; }

    [InverseProperty("PromoCode")]
    [Display(AutoGenerateField = false)]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    
    public override string ToString()
    {
        return $"Код: {Code}, Максимум использования: {MaxUsages}, Процент Скидки: {DiscountPercent}";
    }
}
