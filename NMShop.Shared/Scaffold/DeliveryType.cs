using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NMShop.Shared.Scaffold;

[Table("DeliveryTypes", Schema = "NMShop")]
[Index("Name", Name = "DeliveryTypes_Name_key", IsUnique = true)]
public partial class DeliveryType
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [InverseProperty("DeliveryType")]
    
    [Display(AutoGenerateField = false)]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    
    public override string ToString()
    {
        return $"тип доставки: {Name}";
    }
}