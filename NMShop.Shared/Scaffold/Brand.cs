using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NMShop.Shared.Scaffold;

[Table("Brands", Schema = "NMShop")]
[Index("Name", Name = "Brands_Name_key", IsUnique = true)]
public partial class Brand
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [InverseProperty("Brand")]
    [Display(AutoGenerateField = false)]
    public virtual ICollection<BrandGalleryItem> BrandGalleryItems { get; set; } = new List<BrandGalleryItem>();

    [InverseProperty("Brand")]
    [Display(AutoGenerateField = false)]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    
    public override string ToString()
    {
        return $"{Id} + {Name}";
    }
}