using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NMShop.Scaffold;

[Table("OrderParts", Schema = "NMShop")]
public partial class OrderPart
{
    [Key]
    public int Id { get; set; }

    [Column("Order_Id")]
    public int OrderId { get; set; }

    [Column("Product_Id")]
    public int ProductId { get; set; }

    public int Amount { get; set; }

    [ForeignKey("OrderId")]
    [InverseProperty("OrderParts")]
    public virtual Order Order { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("OrderParts")]
    public virtual Product Product { get; set; } = null!;
}
