using System;
using System.Collections.Generic;

namespace NMShop.Shared.Scaffold;

public partial class PromoCode
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public int MaxUsages { get; set; }

    public int DiscountPercent { get; set; }

    public DateOnly? ExpirationDate { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
