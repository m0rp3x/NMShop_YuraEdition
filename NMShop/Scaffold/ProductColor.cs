using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NMShop.Scaffold;

[Table("ProductColors", Schema = "NMShop")]
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
        return Id.ToString();  // Отображать ID бренда
    }

}
