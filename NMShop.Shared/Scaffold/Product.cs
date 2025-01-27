using System;
using System.Collections.Generic;

namespace NMShop.Shared.Scaffold;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int BrandId { get; set; }

    public string Article { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int GenderId { get; set; }

    public int ProductTypeId { get; set; }

    public int SellingCategoryId { get; set; }

    public DateOnly DateAdded { get; set; }

    public int ColorId { get; set; }

    public virtual Brand Brand { get; set; } = null!;

    public virtual ProductColor Color { get; set; } = null!;

    public virtual Gender Gender { get; set; } = null!;

    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

    public virtual ProductType ProductType { get; set; } = null!;

    public virtual SellingCategory SellingCategory { get; set; } = null!;

    public virtual ICollection<StockInfo> StockInfos { get; set; } = new List<StockInfo>();
}
