using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NMShop.Shared.Scaffold;

[Table("ReferenceContent", Schema = "NMShop")]
public partial class ReferenceContent
{
    [Key]
    public int Id { get; set; }

    [Column("Topic_Id")]
    public int TopicId { get; set; }

    [Column("TextSize_Id")]
    public int TextSizeId { get; set; }

    [StringLength(1000)]
    public string Content { get; set; } = null!;

    public bool IsBold { get; set; }

    [ForeignKey("TextSizeId")]
    [InverseProperty("ReferenceContents")]
    public virtual TextSize TextSize { get; set; } = null!;

    [ForeignKey("TopicId")]
    [InverseProperty("ReferenceContents")]
    public virtual ReferenceTopic Topic { get; set; } = null!;
}
