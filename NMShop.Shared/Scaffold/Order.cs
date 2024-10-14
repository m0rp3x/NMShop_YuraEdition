using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NMShop.Shared.Scaffold;

[Table("Orders", Schema = "NMShop")]
public partial class Order
{
    [Key]
    [DisplayName("Идентификатор")]

    public int Id { get; set; }

    [StringLength(250)]
    [DisplayName("ФИО Клиента")]


    public string ClientFullName { get; set; } = null!;

    [StringLength(11)]
    [DisplayName("Телефон Клиента")]

    public string ClientPhone { get; set; } = null!;

    [StringLength(500)]
    [DisplayName("Адрес доставки")]

    public string DeliveryAdress { get; set; } = null!;

    [Column("DeliveryType_Id")]
    [DisplayName("Тип доставки")]

    public int DeliveryTypeId { get; set; }

    [Column("PaymentType_Id")]
    [DisplayName("Тип оплаты")]
    public int PaymentTypeId { get; set; }

    [Column("OrderStatus_Id")]
    [DisplayName("Статус заказа")]
    public int OrderStatusId { get; set; }

    [StringLength(250)]
    [DisplayName("ФИО получателя")]

    public string DeliveryRecipientFullName { get; set; } = null!;

    [StringLength(11)]
    [DisplayName("Телефон получателя")]

    public string DeliveryRecipientPhone { get; set; } = null!;

    [StringLength(1000)]
    [DisplayName("Коментарий к заказу")]

    public string Comment { get; set; } = null!;

    [ForeignKey("DeliveryTypeId")]
    [InverseProperty("Orders")]
    [Display(AutoGenerateField = false)]
    public virtual DeliveryType DeliveryType { get; set; } = null!;

    [InverseProperty("Order")]
    public virtual ICollection<OrderPart> OrderParts { get; set; } = new List<OrderPart>();

    [ForeignKey("OrderStatusId")]
    [InverseProperty("Orders")]
    [Display(AutoGenerateField = false)]
    public virtual OrderStatus OrderStatus { get; set; } = null!;

    [ForeignKey("PaymentTypeId")]
    [InverseProperty("Orders")]
    [Display(AutoGenerateField = false)]
    public virtual PaymentType PaymentType { get; set; } = null!;

    public override string ToString()
    {
        return $"Заказ #{Id}"; // Отображать ID бренда
    }

}
