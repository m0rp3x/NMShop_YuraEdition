using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NMShop.Shared.Scaffold;

[Table("ProductImages", Schema = "NMShop")]
public partial class ProductImage
{
    [Key]
    public int Id { get; set; }

    public byte[] Bytes { get; set; } = null!;

    [Column("Product_Id")]
    public int ProductId { get; set; }

    public bool IsMain { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("ProductImages")]
    public virtual Product Product { get; set; } = null!;
}
