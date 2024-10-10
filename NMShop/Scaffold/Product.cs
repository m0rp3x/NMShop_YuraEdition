using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NMShop.Scaffold;

[Table("Product", Schema = "NMShop")]
public partial class Product
{
    [Key]
    [DisplayName( "Идентификатор")]

    public int Id { get; set; }

    [StringLength(150)]
    [DisplayName( "Название")]

    public string Name { get; set; } = null!;

    [Column("Brand_Id")]
    [DisplayName( "Названиее Бренда")]
    
    public int BrandId { get; set; }

    [StringLength(150)]
    [DisplayName( "Артикль товара")]

    public string Article { get; set; } = null!;

    [StringLength(1000)]
    [DisplayName( "Описание товара")]

    public string Description { get; set; } = null!;

    [Column("Gender_Id")]
    [DisplayName( "Гендер")]

    public int GenderId { get; set; }

    [Column("ProductType_Id")]
    [DisplayName( "тип продукта")]

    public int ProductTypeId { get; set; }

    [Column("SellingCategory_Id")]
    [DisplayName( "Категория продажи")]

    public int SellingCategoryId { get; set; }
    [DisplayName( "Дата добавления")]
    public DateOnly DateAdded { get; set; }

    [Column("Color_Id")]
    [DisplayName( "Цвет")]

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
    public virtual ICollection<OrderPart> OrderParts { get; set; } = new List<OrderPart>();

    [InverseProperty("Product")]
    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

    [ForeignKey("ProductTypeId")]
    [InverseProperty("Products")]
    public virtual ProductType ProductType { get; set; } = null!;

    [ForeignKey("SellingCategoryId")]
    [InverseProperty("Products")]
    public virtual SellingCategory SellingCategory { get; set; } = null!;

    [InverseProperty("Product")]
    [Display(AutoGenerateField = false)]
    public virtual ICollection<StockInfo> StockInfos { get; set; } = new List<StockInfo>();
    
    public override string ToString()
    {
        return $"#{Id} - {Name}"; // Отображать ID бренда
    }

}
