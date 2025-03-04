using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NMShop.Shared.Scaffold;

[Table("StockInfo", Schema = "NMShop")]
public partial class StockInfo
{
    [Key]
    public int Id { get; set; }

    [Column("Product_Id")]
    public int ProductId { get; set; }

    [Precision(10, 0)]
    public decimal Size { get; set; }

    [Precision(10, 0)]
    public decimal Price { get; set; }

    [Precision(10, 0)]
    public decimal? DiscountPrice { get; set; }

    public int AmountInStock { get; set; }

    [InverseProperty("StockInfo")]
    [Display(AutoGenerateField = false)]
    public virtual ICollection<OrderPart> OrderParts { get; set; } = new List<OrderPart>();

    [ForeignKey("ProductId")]
    [InverseProperty("StockInfos")]
    
    [Display(AutoGenerateField = false)]
    public virtual Product Product { get; set; } = null!;
    
    public override string ToString()
    {
        return Product.Name + " - " + Size.ToString() + " x " + Price.ToString();
    }
}