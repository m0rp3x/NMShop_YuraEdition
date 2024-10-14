using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NMShop.Shared.Scaffold;

[Table("Genders", Schema = "NMShop")]
public partial class Gender
{
    [Key]
    [DisplayName("Идентификатор")]

    public int Id { get; set; }

    [StringLength(50)]
    [DisplayName("Название")]
    public string Name { get; set; } = null!;

    [InverseProperty("Gender")]
    [Display(AutoGenerateField = false)]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public override string ToString()
    {
        return $"{Name}";  // Отображать ID бренда
    }

}
