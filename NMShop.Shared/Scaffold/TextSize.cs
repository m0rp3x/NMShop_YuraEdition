using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NMShop.Shared.Scaffold;

[Table("TextSizes", Schema = "NMShop")]
public partial class TextSize
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [DisplayName("Идентификатор размера текста")]
    public int Id { get; set; }

    [StringLength(50)]
    [DisplayName("Значение размера текста")]
    public string Value { get; set; } = null!;

    [InverseProperty("TextSize")]
    [Display(AutoGenerateField = false)]
    public virtual ICollection<ReferenceContent> ReferenceContents { get; set; } = new List<ReferenceContent>();

    public override string ToString()
    {
        return $"Размер текста: {Value}";
    }
}
