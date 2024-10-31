using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NMShop.Shared.Scaffold;

[Table("ReferenceTopics", Schema = "NMShop")]
public partial class ReferenceTopic
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Code { get; set; } = null!;

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("ParentTopic_Id")]
    public int? ParentTopicId { get; set; }

    [InverseProperty("ParentTopic")]
    public virtual ICollection<ReferenceTopic> InverseParentTopic { get; set; } = new List<ReferenceTopic>();

    [ForeignKey("ParentTopicId")]
    [InverseProperty("InverseParentTopic")]
    public virtual ReferenceTopic? ParentTopic { get; set; }

    [InverseProperty("Topic")]
    public virtual ICollection<ReferenceContent> ReferenceContents { get; set; } = new List<ReferenceContent>();
}
