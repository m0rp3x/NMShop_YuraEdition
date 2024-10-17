using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NMShop.Shared.Scaffold;

[Table("NavigationItems", Schema = "NMShop")]
public partial class NavigationItem
{
    [Key]
    [DisplayName("Идентификатор пункта навигации")]
    public int Id { get; set; }

    [StringLength(100)]
    [DisplayName("Название пункта навигации")]
    public string Name { get; set; } = null!;

    [StringLength(255)]
    [DisplayName("Ссылка пункта навигации")]
    public string Link { get; set; } = null!;

    [Column("ParentItem_Id")]
    [DisplayName("Родительский пункт навигации")]
    public int? ParentItemId { get; set; }

    [InverseProperty("ParentItem")]
    [Display(AutoGenerateField = false)]
    public virtual ICollection<NavigationItem> InverseParentItem { get; set; } = new List<NavigationItem>();

    [ForeignKey("ParentItemId")]
    [InverseProperty("InverseParentItem")]
    [Display(AutoGenerateField = false)]
    public virtual NavigationItem? ParentItem { get; set; }

    public override string ToString()
    {
        return $"Пункт навигации: {Name}, Ссылка: {Link}";
    }
}
