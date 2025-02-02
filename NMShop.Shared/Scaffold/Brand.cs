using System;
using System.Collections.Generic;

namespace NMShop.Shared.Scaffold;

public partial class Brand
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<BrandGalleryItem> BrandGalleryItems { get; set; } = new List<BrandGalleryItem>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
