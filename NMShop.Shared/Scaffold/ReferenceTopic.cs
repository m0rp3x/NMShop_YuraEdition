using System;
using System.Collections.Generic;

namespace NMShop.Shared.Scaffold;

public partial class ReferenceTopic
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int? ParentTopicId { get; set; }

    public virtual ICollection<ReferenceTopic> InverseParentTopic { get; set; } = new List<ReferenceTopic>();

    public virtual ReferenceTopic? ParentTopic { get; set; }

    public virtual ICollection<ReferenceContent> ReferenceContents { get; set; } = new List<ReferenceContent>();
}
