using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace NMShop.Scaffold;

[Table("Orders", Schema = "NMShop")]
public partial class Order
{
    [Key]
    public int Id { get; set; }

    [StringLength(250)]
    public string ClientFullName { get; set; } = null!;

    [StringLength(11)]
    public string ClientPhone { get; set; } = null!;

    [StringLength(500)]
    public string DeliveryAdress { get; set; } = null!;

    [Column("DeliveryType_Id")]
    public int DeliveryTypeId { get; set; }

    [Column("PaymentType_Id")]
    public int PaymentTypeId { get; set; }

    [Column("OrderStatus_Id")]
    public int OrderStatusId { get; set; }

    [StringLength(250)]
    public string DeliveryRecipientFullName { get; set; } = null!;

    [StringLength(11)]
    public string DeliveryRecipientPhone { get; set; } = null!;

    [StringLength(1000)]
    public string Comment { get; set; } = null!;

    [ForeignKey("DeliveryTypeId")]
    [InverseProperty("Orders")]
    [JsonIgnore]

    
    public virtual DeliveryType DeliveryType { get; set; } = null!;

    [InverseProperty("Order")]
    [JsonIgnore]

    public virtual ICollection<OrderPart> OrderParts { get; set; } = new List<OrderPart>();

    [ForeignKey("OrderStatusId")]
    [InverseProperty("Orders")]
    [JsonIgnore]

    public virtual OrderStatus OrderStatus { get; set; } = null!;

    [ForeignKey("PaymentTypeId")]
    [InverseProperty("Orders")]
    [JsonIgnore]

    public virtual PaymentType PaymentType { get; set; } = null!;
    
 
}
