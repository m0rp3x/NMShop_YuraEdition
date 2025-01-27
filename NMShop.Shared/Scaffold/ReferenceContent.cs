using System;
using System.Collections.Generic;

namespace NMShop.Shared.Scaffold;

public partial class ReferenceContent
{
    public int Id { get; set; }

    public int TopicId { get; set; }

    public int TextSizeId { get; set; }

    public string Content { get; set; } = null!;

    public bool IsBold { get; set; }

    public virtual TextSize TextSize { get; set; } = null!;

    public virtual ReferenceTopic Topic { get; set; } = null!;
}
