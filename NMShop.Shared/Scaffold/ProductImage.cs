using System;
using System.Collections.Generic;

namespace NMShop.Shared.Scaffold;

public partial class ProductImage
{
    public int Id { get; set; }

    public byte[] Bytes { get; set; } = null!;

    public int ProductId { get; set; }

    public bool IsMain { get; set; }

    public virtual Product Product { get; set; } = null!;
}
