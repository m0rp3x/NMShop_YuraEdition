using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NMShop.Scaffold;

[Table("StockInfo", Schema = "NMShop")]
public partial class StockInfo
{
    [Key]
    [DisplayName( "Идентификатор")]

    public int Id { get; set; }
    [DisplayName( "Название продукта")]


    [Column("Product_Id")]
    public int ProductId { get; set; }

    [Precision(10, 0)]
    [DisplayName( "Размер")]

    public decimal Size { get; set; }

    [Precision(10, 0)]
    [DisplayName( "Цена")]

    public decimal Price { get; set; }

    [Precision(10, 0)]
    [DisplayName( "Акционная цена")]

    public decimal? DiscountPrice { get; set; }
    [DisplayName( "Количество в наличие")]
    public int AmountInStock { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("StockInfos")]
    [Display(AutoGenerateField = false)]
    public virtual Product Product { get; set; } = null!;
    
    public override string ToString()
    {
        return $"Id продукта: {ProductId} количество: {AmountInStock}"; // Отображать ID бренда
    }

}
