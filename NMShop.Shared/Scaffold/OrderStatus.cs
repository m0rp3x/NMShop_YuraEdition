using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NMShop.Shared.Scaffold;

[Table("OrderStatuses", Schema = "NMShop")]
public partial class OrderStatus
{
    [Key]
    [DisplayName("Идентификатор")]

    public int Id { get; set; }

    [StringLength(100)]
    [DisplayName("Название")]

    public string Name { get; set; } = null!;

    [InverseProperty("OrderStatus")]
    [Display(AutoGenerateField = false)]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public override string ToString()
    {
        return Name.ToString();  // Отображать ID бренда
    }

}
