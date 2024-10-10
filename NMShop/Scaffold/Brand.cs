using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NMShop.Scaffold;

[Table("Brands", Schema = "NMShop")]
public partial class Brand
{
    [Key]
    [DisplayName ("Идентификатор")]
    public int Id { get; set; }

    [StringLength(100)]
    [DisplayName ("Название")]
    public string Name { get; set; } = null!;

    [InverseProperty("Brand")]
    [Display(AutoGenerateField = false)]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    
    public override string ToString()
    {
        return $"Бренд {Name}";  // Отображать ID бренда
    }


}

