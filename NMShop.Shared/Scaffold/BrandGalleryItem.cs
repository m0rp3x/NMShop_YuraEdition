using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NMShop.Shared.Scaffold;

[Table("BrandGalleryItems", Schema = "NMShop")]
public partial class BrandGalleryItem
{
    [Key]
    public int Id { get; set; }

    [Column("Brand_Id")]
    public int BrandId { get; set; }

    public byte[] Image { get; set; } = null!;

    [ForeignKey("BrandId")]
    [InverseProperty("BrandGalleryItems")]
    
    [Display(AutoGenerateField = false)]
    public virtual Brand Brand { get; set; } = null!;
}