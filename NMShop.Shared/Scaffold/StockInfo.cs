using System;
using System.Collections.Generic;

namespace NMShop.Shared.Scaffold;

public partial class StockInfo
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public decimal Size { get; set; }

    public decimal Price { get; set; }

    public decimal? DiscountPrice { get; set; }

    public int AmountInStock { get; set; }

    public virtual ICollection<OrderPart> OrderParts { get; set; } = new List<OrderPart>();

    public virtual Product Product { get; set; } = null!;
}
