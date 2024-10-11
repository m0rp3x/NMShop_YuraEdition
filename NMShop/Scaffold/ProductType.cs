using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NMShop.Scaffold;

[Table("ProductTypes", Schema = "NMShop")]
public partial class ProductType
{
    [Key]
    [DisplayName("Идентификатор")]

    public int Id { get; set; }

    [StringLength(50)]
    [DisplayName("Название")]

    public string Name { get; set; } = null!;

    [Column("ParentType_Id")]
    [DisplayName("Родительский тип продукта")]

    public int? ParentTypeId { get; set; }

    [DisplayName("Тип отображения размера")]
    [StringLength(10)]
    public string? SizeDisplayType { get; set; }

    [InverseProperty("ParentType")]
    [Display(AutoGenerateField = false)]
    public virtual ICollection<ProductType> InverseParentType { get; set; } = new List<ProductType>();

    [ForeignKey("ParentTypeId")]
    [InverseProperty("InverseParentType")]
    [Display(AutoGenerateField = false)]
    public virtual ProductType? ParentType { get; set; }

    [InverseProperty("ProductType")]
    [Display(AutoGenerateField = false)]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    
    public override string ToString()
    {
        return Name + (ParentType is null ? "" : $", родительский - {ParentType.Name}" );  // Отображать ID бренда
    }

}
