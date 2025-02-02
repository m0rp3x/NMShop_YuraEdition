using System;
using System.Collections.Generic;

namespace NMShop.Shared.Scaffold;

public partial class ProductColor
{
    public int Id { get; set; }

    public string Value { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
