using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NMShop.Shared.Scaffold;

[Table("ContactMethods", Schema = "NMShop")]
[Index("Name", Name = "ContactMethods_Name_key", IsUnique = true)]
public partial class ContactMethod
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [StringLength(255)]
    public string? ValidationMask { get; set; }

    [StringLength(255)]
    public string? ValidationErrorText { get; set; }

    [InverseProperty("ContactMethod")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
