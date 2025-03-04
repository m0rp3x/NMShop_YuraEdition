using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NMShop.Shared.Scaffold;

[Table("PaymentTypes", Schema = "NMShop")]
[Index("Name", Name = "PaymentTypes_Name_key", IsUnique = true)]
public partial class PaymentType
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [InverseProperty("PaymentType")]
    [Display(AutoGenerateField = false)]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    
    public override string ToString()
    {
        return $"Тип оплаты: {Name}";
    }
}