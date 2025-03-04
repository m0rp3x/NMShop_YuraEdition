using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NMShop.Shared.Scaffold;

[Table("SellingCategories", Schema = "NMShop")]
[Index("Name", Name = "SellingCategories_Name_key", IsUnique = true)]
public partial class SellingCategory
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [InverseProperty("SellingCategory")]
    [Display(AutoGenerateField = false)]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    public override string ToString()
    {
        return $"Категория: {Name}";
    }
}