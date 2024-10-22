using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NMShop.Shared.Scaffold;

[Table("Brands", Schema = "NMShop")]
public partial class Brand
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [DisplayName("Идентификатор")]
    public int Id { get; set; }

    [StringLength(100)]
    [DisplayName("Название")]
    public string Name { get; set; } = null!;

    [InverseProperty("Brand")]
    public virtual ICollection<BrandGallery> BrandGalleries { get; set; } = new List<BrandGallery>();

    [InverseProperty("Brand")]
    [Display(AutoGenerateField = false)]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public override string ToString()
    {
        return $"Бренд {Name}";  // Отображать ID бренда
    }


}

