using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NMShop.Shared.Scaffold;

[Table("ContactMethods", Schema = "NMShop")]
public partial class ContactMethod
{
    [Key]
    [DisplayName("Идентификатор метода связи")]
    public int Id { get; set; }

    [StringLength(100)]
    [DisplayName("Название метода связи")]
    public string Name { get; set; } = null!;

    [StringLength(255)]
    [DisplayName("Маска валидации")]
    public string? ValidationMask { get; set; }

    [StringLength(255)]
    [DisplayName("Текст ошибки валидации")]
    public string? ValidationErrorText { get; set; }

    [InverseProperty("ContactMethod")]
    [Display(AutoGenerateField = false)]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public override string ToString()
    {
        return $"Метод связи: {Name}, Маска валидации: {(string.IsNullOrEmpty(ValidationMask) ? "отсутствует" : ValidationMask)}, Текст ошибки: {(string.IsNullOrEmpty(ValidationErrorText) ? "отсутствует" : ValidationErrorText)}";
    }
}
