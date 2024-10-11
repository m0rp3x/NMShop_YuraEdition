using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NMShop.Scaffold;

[Table("DeliveryTypes", Schema = "NMShop")]
public partial class DeliveryType
{
    [Key]
    [DisplayName  ("Идентификатор")]
    public int Id { get; set; }

    [StringLength(100)]
    [DisplayName  ("Название")]
    public string Name { get; set; } = null!;

    [InverseProperty("DeliveryType")]
    [Display(AutoGenerateField = false)]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    
    public override string ToString()
    {
        return $"{Name}";  // Отображать ID бренда
    }


}
