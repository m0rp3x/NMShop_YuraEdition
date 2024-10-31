using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NMShop.Shared.Scaffold;

[Table("Product", Schema = "NMShop")]
[Index("Article", Name = "Product_Article_key", IsUnique = true)]
public partial class Product
{
    [Key]
    public int Id { get; set; }

    [StringLength(150)]
    public string Name { get; set; } = null!;

    [Column("Brand_Id")]
    public int BrandId { get; set; }

    [StringLength(150)]
    public string Article { get; set; } = null!;

    [StringLength(1000)]
    public string Description { get; set; } = null!;

    [Column("Gender_Id")]
    public int GenderId { get; set; }

    [Column("ProductType_Id")]
    public int ProductTypeId { get; set; }

    [Column("SellingCategory_Id")]
    public int SellingCategoryId { get; set; }

    public DateOnly DateAdded { get; set; }

    [Column("Color_Id")]
    public int ColorId { get; set; }

    [ForeignKey("BrandId")]
    [InverseProperty("Products")]
    public virtual Brand Brand { get; set; } = null!;

    [ForeignKey("ColorId")]
    [InverseProperty("Products")]
    public virtual ProductColor Color { get; set; } = null!;

    [ForeignKey("GenderId")]
    [InverseProperty("Products")]
    public virtual Gender Gender { get; set; } = null!;

    [InverseProperty("Product")]
    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

    [ForeignKey("ProductTypeId")]
    [InverseProperty("Products")]
    public virtual ProductType ProductType { get; set; } = null!;

    [ForeignKey("SellingCategoryId")]
    [InverseProperty("Products")]
    public virtual SellingCategory SellingCategory { get; set; } = null!;

    [InverseProperty("Product")]
    public virtual ICollection<StockInfo> StockInfos { get; set; } = new List<StockInfo>();
}
