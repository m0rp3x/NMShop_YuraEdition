using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NMShop.Shared.Scaffold;

[Table("SellingCategories", Schema = "NMShop")]
public partial class SellingCategory
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [DisplayName("Идентификатор")]

    public int Id { get; set; }

    [StringLength(50)]
    [DisplayName("Название")]

    public string Name { get; set; } = null!;

    [InverseProperty("SellingCategory")]
    [Display(AutoGenerateField = false)]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public override string ToString()
    {
        return $"{Name}";  // Отображать ID бренда
    }

}
