using System;
using System.Collections.Generic;

namespace NMShop.Shared.Scaffold;

public partial class ProductType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? ParentTypeId { get; set; }

    public string? SizeDisplayType { get; set; }

    public virtual ICollection<ProductType> InverseParentType { get; set; } = new List<ProductType>();

    public virtual ProductType? ParentType { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
