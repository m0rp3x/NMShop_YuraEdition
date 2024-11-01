using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NMShop.Shared.Scaffold;

[Table("Orders", Schema = "NMShop")]
public partial class Order
{
    [Key]
    public int Id { get; set; }

    [StringLength(250)]
    public string ClientFullName { get; set; } = null!;

    [StringLength(500)]
    public string DeliveryAdress { get; set; } = null!;

    [Column("DeliveryType_Id")]
    public int DeliveryTypeId { get; set; }

    [Column("PaymentType_Id")]
    public int PaymentTypeId { get; set; }

    [StringLength(50)]
    public string? EstimatedDeliveryDateRange { get; set; }

    [Column("OrderStatus_Id")]
    public int OrderStatusId { get; set; }

    [StringLength(250)]
    public string DeliveryRecipientFullName { get; set; } = null!;

    [StringLength(11)]
    public string DeliveryRecipientPhone { get; set; } = null!;

    [StringLength(1000)]
    public string? Comment { get; set; }

    [Column("ContactMethod_Id")]
    public int ContactMethodId { get; set; }

    [StringLength(255)]
    public string ContactValue { get; set; } = null!;

    [Column("PromoCode_Id")]
    public int? PromoCodeId { get; set; }

    public decimal? Total { get; set; }

    [ForeignKey("ContactMethodId")]
    [InverseProperty("Orders")]
    public virtual ContactMethod ContactMethod { get; set; } = null!;

    [ForeignKey("DeliveryTypeId")]
    [InverseProperty("Orders")]
    public virtual DeliveryType DeliveryType { get; set; } = null!;

    [InverseProperty("Order")]
    public virtual ICollection<OrderPart> OrderParts { get; set; } = new List<OrderPart>();

    [ForeignKey("OrderStatusId")]
    [InverseProperty("Orders")]
    public virtual OrderStatus OrderStatus { get; set; } = null!;

    [ForeignKey("PaymentTypeId")]
    [InverseProperty("Orders")]
    public virtual PaymentType PaymentType { get; set; } = null!;

    [ForeignKey("PromoCodeId")]
    [InverseProperty("Orders")]
    public virtual PromoCode? PromoCode { get; set; }
}
