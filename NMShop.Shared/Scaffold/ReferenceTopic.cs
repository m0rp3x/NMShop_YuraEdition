using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NMShop.Shared.Scaffold;

[Table("ReferenceTopics", Schema = "NMShop")]
public partial class ReferenceTopic
{
    [Key]
    [DisplayName("Идентификатор топика")]
    public int Id { get; set; }

    [StringLength(50)]
    [DisplayName("Код топика")]
    public string Code { get; set; } = null!;

    [StringLength(100)]
    [DisplayName("Название топика")]
    public string Name { get; set; } = null!;

    [Column("ParentTopic_Id")]
    [DisplayName("Родительский топик")]
    public int? ParentTopicId { get; set; }

    [InverseProperty("ParentTopic")]
    [Display(AutoGenerateField = false)]
    public virtual ICollection<ReferenceTopic> InverseParentTopic { get; set; } = new List<ReferenceTopic>();

    [ForeignKey("ParentTopicId")]
    [InverseProperty("InverseParentTopic")]
    [Display(AutoGenerateField = false)]
    public virtual ReferenceTopic? ParentTopic { get; set; }

    [InverseProperty("Topic")]
    [Display(AutoGenerateField = false)]
    public virtual ICollection<ReferenceContent> ReferenceContents { get; set; } = new List<ReferenceContent>();

    public override string ToString()
    {
        return $"Топик: {Name} (Код: {Code})";
    }
}
