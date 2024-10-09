using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NMShop.Scaffold;

[Table("SellingCategories", Schema = "NMShop")]
public partial class SellingCategory
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [InverseProperty("SellingCategory")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
