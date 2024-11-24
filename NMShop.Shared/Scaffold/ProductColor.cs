using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NMShop.Shared.Scaffold;

[Table("ProductColors", Schema = "NMShop")]
[Index("Name", Name = "ProductColors_Name_key", IsUnique = true)]
[Index("Value", Name = "ProductColors_Value_key", IsUnique = true)]
public partial class ProductColor
{
    [Key]
    public int Id { get; set; }

    [StringLength(6)]
    public string Value { get; set; } = null!;

    [StringLength(30)]
    public string Name { get; set; } = null!;

    [InverseProperty("Color")]
    [Display(AutoGenerateField = false)]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    
    public override string ToString()
    {
        return $"Цвет: Name: {Name}, Value: {Value}";
    }
}
