using System;
using System.Collections.Generic;

namespace NMShop.Shared.Scaffold;

public partial class TextSize
{
    public int Id { get; set; }

    public string Value { get; set; } = null!;

    public virtual ICollection<ReferenceContent> ReferenceContents { get; set; } = new List<ReferenceContent>();
}
