using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NMShop.Shared.Scaffold;

[Table("ProductImages", Schema = "NMShop")]
public partial class ProductImage
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [DisplayName("Идентификатор")]

    public int Id { get; set; }

    [DisplayName("Изображение")]

    public byte[] Bytes { get; set; } = null!;

    [Column("Product_Id")]
    [DisplayName("Название продукта")]

    public int ProductId { get; set; }
    [DisplayName("Основной")]

    public bool IsMain { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("ProductImages")]
    [Display(AutoGenerateField = false)]
    public virtual Product Product { get; set; } = null!;

    public override string ToString()
    {
        return $"Изображение продукта: {Product.Name} Идентификатор продукта: {ProductId}";  // Отображать ID бренда
    }

}
