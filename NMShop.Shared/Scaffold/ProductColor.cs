using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NMShop.Shared.Scaffold;

[Table("ProductColors", Schema = "NMShop")]
public partial class ProductColor
{
    [Key]
    [DisplayName("Идентификатор")]

    public int Id { get; set; }

    [StringLength(6)]
    [DisplayName("Код цвета")]

    public string Value { get; set; } = null!;

    [StringLength(30)]
    [DisplayName("Название")]

    public string Name { get; set; } = null!;

    [InverseProperty("Color")]
    [Display(AutoGenerateField = false)]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public override string ToString()
    {
        return $"{Name}";  // Отображать ID бренда
    }

}
