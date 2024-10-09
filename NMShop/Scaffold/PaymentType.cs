using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NMShop.Scaffold;

[Table("PaymentTypes", Schema = "NMShop")]
public partial class PaymentType
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [InverseProperty("PaymentType")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
