using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NMShop.Shared.Scaffold;

[Table("ReferenceContent", Schema = "NMShop")]
public partial class ReferenceContent
{
    [Key]
    [DisplayName("Идентификатор контента")]
    public int Id { get; set; }

    [Column("Topic_Id")]
    [DisplayName("Топик контента")]
    public int TopicId { get; set; }

    [Column("TextSize_Id")]
    [DisplayName("Размер текста")]
    public int TextSizeId { get; set; }

    [StringLength(1000)]
    [DisplayName("Содержимое контента")]
    public string Content { get; set; } = null!;

    [DisplayName("Жирный шрифт")]
    public bool IsBold { get; set; }

    [ForeignKey("TextSizeId")]
    [InverseProperty("ReferenceContents")]
    [Display(AutoGenerateField = false)]
    public virtual TextSize TextSize { get; set; } = null!;

    [ForeignKey("TopicId")]
    [InverseProperty("ReferenceContents")]
    [Display(AutoGenerateField = false)]
    public virtual ReferenceTopic Topic { get; set; } = null!;

    public override string ToString()
    {
        return $"Контент (Топик: {Topic?.Name}, Размер текста: {TextSize?.Value}, Жирный: {(IsBold ? "Да" : "Нет")})";
    }
}
