using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NMShop.Scaffold;

[Table("OrderParts", Schema = "NMShop")]
public partial class OrderPart
{
    [Key]
    [DisplayName( "Идентификатор")]
    public int Id { get; set; }

    [Column("Order_Id")]
    [DisplayName( "Идентификатор заказа")]
    
    public int OrderId { get; set; }

    [Column("Product_Id")]
    [DisplayName( "Продукт")]

    public int ProductId { get; set; }
    [DisplayName( "Количество")]

    public int Amount { get; set; }

    [ForeignKey("OrderId")]
    [InverseProperty("OrderParts")]
    [Display(AutoGenerateField = false)]
    public virtual Order Order { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("OrderParts")]
    [Display(AutoGenerateField = false)]
    public virtual Product Product { get; set; } = null!;
    
    public override string ToString()
    {
        return Id.ToString() + Product.Name;  // Отображать ID бренда
    }

}
