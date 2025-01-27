using System;
using System.Collections.Generic;

namespace NMShop.Shared.Scaffold;

public partial class NavigationItem
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Link { get; set; } = null!;

    public int? ParentItemId { get; set; }

    public virtual ICollection<NavigationItem> InverseParentItem { get; set; } = new List<NavigationItem>();

    public virtual NavigationItem? ParentItem { get; set; }
}
