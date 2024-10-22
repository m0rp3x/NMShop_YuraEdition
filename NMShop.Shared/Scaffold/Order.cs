using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NMShop.Shared.Scaffold;

[Table("Orders", Schema = "NMShop")]
public partial class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [DisplayName("Идентификатор заказа")]
    public int Id { get; set; }

    [StringLength(250)]
    [DisplayName("ФИО клиента")]
    public string ClientFullName { get; set; } = null!;

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
    
    [StringLength(50)]
    [DisplayName("Ориентировочная дата доставки")]
    public string? EstimatedDeliveryDateRange { get; set; } = null!;
    
    [StringLength(250)]
    [DisplayName("ФИО получателя")]
    public string DeliveryRecipientFullName { get; set; } = null!;

    [StringLength(11)]
    [DisplayName("Телефон получателя")]
    public string DeliveryRecipientPhone { get; set; } = null!;

    [StringLength(1000)]
    [DisplayName("Комментарий к заказу")]
    public string? Comment { get; set; } = null!;

    [Column("ContactMethod_Id")]
    [DisplayName("Способ связи")]
    public int ContactMethodId { get; set; }

    [StringLength(255)]
    [DisplayName("Контактная информация")]
    public string ContactValue { get; set; } = null!;

    [Column("PromoCode_Id")]
    [DisplayName("Промокод")]
    public int? PromoCodeId { get; set; }

    [ForeignKey("ContactMethodId")]
    [InverseProperty("Orders")]
    [Display(AutoGenerateField = false)]
    public virtual ContactMethod ContactMethod { get; set; } = null!;

    [ForeignKey("DeliveryTypeId")]
    [InverseProperty("Orders")]
    [Display(AutoGenerateField = false)]
    public virtual DeliveryType DeliveryType { get; set; } = null!;

    [InverseProperty("Order")]
    [Display(AutoGenerateField = false)]
    public virtual ICollection<OrderPart> OrderParts { get; set; } = new List<OrderPart>();

    [ForeignKey("OrderStatusId")]
    [InverseProperty("Orders")]
    [Display(AutoGenerateField = false)]
    public virtual OrderStatus OrderStatus { get; set; } = null!;

    [ForeignKey("PaymentTypeId")]
    [InverseProperty("Orders")]
    [Display(AutoGenerateField = false)]
    public virtual PaymentType PaymentType { get; set; } = null!;

    [ForeignKey("PromoCodeId")]
    [InverseProperty("Orders")]
    [Display(AutoGenerateField = false)]
    public virtual PromoCode? PromoCode { get; set; }
}
