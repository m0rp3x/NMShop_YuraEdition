using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NMShop.Scaffold;

[Table("ProductTypes", Schema = "NMShop")]
public partial class ProductType
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column("ParentType_Id")]
    public int? ParentTypeId { get; set; }

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
        return Id.ToString();  // Отображать ID бренда
    }

}
