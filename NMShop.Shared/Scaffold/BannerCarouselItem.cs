using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NMShop.Shared.Scaffold;

[Table("BannerCarouselItems", Schema = "NMShop")]
public partial class BannerCarouselItem
{
    [Key]
    public int Id { get; set; }

    public byte[] Image { get; set; } = null!;
}
