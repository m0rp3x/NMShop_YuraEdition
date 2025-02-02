using System;
using System.Collections.Generic;

namespace NMShop.Shared.Scaffold;

public partial class ContactMethod
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? ValidationMask { get; set; }

    public string? ValidationErrorText { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
