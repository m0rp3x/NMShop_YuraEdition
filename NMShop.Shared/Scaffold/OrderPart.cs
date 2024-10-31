using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NMShop.Shared.Scaffold;

[Table("OrderParts", Schema = "NMShop")]
public partial class OrderPart
{
    [Key]
    public int Id { get; set; }

    [Column("Order_Id")]
    public int OrderId { get; set; }

    [Column("StockInfo_Id")]
    public int StockInfoId { get; set; }

    public int Amount { get; set; }

    [ForeignKey("OrderId")]
    [InverseProperty("OrderParts")]
    public virtual Order Order { get; set; } = null!;

    [ForeignKey("StockInfoId")]
    [InverseProperty("OrderParts")]
    public virtual StockInfo StockInfo { get; set; } = null!;
}
