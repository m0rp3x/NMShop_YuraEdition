using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NMShop.Shared.Scaffold;

[Table("NavigationItems", Schema = "NMShop")]
public partial class NavigationItem
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [StringLength(255)]
    public string Link { get; set; } = null!;

    [Column("ParentItem_Id")]
    public int? ParentItemId { get; set; }

    [InverseProperty("ParentItem")]
    
    [Display(AutoGenerateField = false)]
    public virtual ICollection<NavigationItem> InverseParentItem { get; set; } = new List<NavigationItem>();

    [ForeignKey("ParentItemId")]
    [InverseProperty("InverseParentItem")]
    
    [Display(AutoGenerateField = false)]
    public virtual NavigationItem? ParentItem { get; set; }
    
    
}
