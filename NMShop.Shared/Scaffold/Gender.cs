using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NMShop.Shared.Scaffold;

[Table("Genders", Schema = "NMShop")]
[Index("Name", Name = "Genders_Name_key", IsUnique = true)]
public partial class Gender
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [InverseProperty("Gender")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
