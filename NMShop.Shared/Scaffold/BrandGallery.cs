using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NMShop.Shared.Scaffold;

[Table("BrandGallery", Schema = "NMShop")]
public partial class BrandGallery
{
    [Key]
    [DisplayName("Идентификатор записи галереи бренда")]
    public int Id { get; set; }

    [Column("Brand_Id")]
    [DisplayName("Идентификатор бренда")]
    public int BrandId { get; set; }

    [DisplayName("Изображение бренда")]
    public byte[] Image { get; set; } = null!;

    [ForeignKey("BrandId")]
    [InverseProperty("BrandGalleries")]
    [Display(AutoGenerateField = false)]
    public virtual Brand Brand { get; set; } = null!;

    public override string ToString()
    {
        return $"Галерея бренда (ID бренда: {BrandId})";
    }
}
