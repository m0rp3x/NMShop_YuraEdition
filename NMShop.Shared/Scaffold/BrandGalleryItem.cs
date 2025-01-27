using System;
using System.Collections.Generic;

namespace NMShop.Shared.Scaffold;

public partial class BrandGalleryItem
{
    public int Id { get; set; }

    public int BrandId { get; set; }

    public byte[] Image { get; set; } = null!;

    public virtual Brand Brand { get; set; } = null!;
}
