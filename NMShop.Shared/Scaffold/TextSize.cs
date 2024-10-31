using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NMShop.Shared.Scaffold;

[Table("TextSizes", Schema = "NMShop")]
[Index("Value", Name = "TextSizes_Value_key", IsUnique = true)]
public partial class TextSize
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Value { get; set; } = null!;

    [InverseProperty("TextSize")]
    public virtual ICollection<ReferenceContent> ReferenceContents { get; set; } = new List<ReferenceContent>();
}
