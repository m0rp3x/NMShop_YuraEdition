using System;
using System.Collections.Generic;

namespace NMShop.Shared.Scaffold;

public partial class OrderPart
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int StockInfoId { get; set; }

    public int Amount { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual StockInfo StockInfo { get; set; } = null!;
}
