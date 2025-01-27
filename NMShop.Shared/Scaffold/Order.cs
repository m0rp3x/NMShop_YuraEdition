using System;
using System.Collections.Generic;

namespace NMShop.Shared.Scaffold;

public partial class Order
{
    public int Id { get; set; }

    public string ClientFullName { get; set; } = null!;

    public string DeliveryAdress { get; set; } = null!;

    public int DeliveryTypeId { get; set; }

    public int PaymentTypeId { get; set; }

    public string? EstimatedDeliveryDateRange { get; set; }

    public int OrderStatusId { get; set; }

    public string DeliveryRecipientFullName { get; set; } = null!;

    public string DeliveryRecipientPhone { get; set; } = null!;

    public string? Comment { get; set; }

    public int ContactMethodId { get; set; }

    public string ContactValue { get; set; } = null!;

    public int? PromoCodeId { get; set; }

    public decimal? Total { get; set; }

    public virtual ContactMethod ContactMethod { get; set; } = null!;

    public virtual DeliveryType DeliveryType { get; set; } = null!;

    public virtual ICollection<OrderPart> OrderParts { get; set; } = new List<OrderPart>();

    public virtual OrderStatus OrderStatus { get; set; } = null!;

    public virtual PaymentType PaymentType { get; set; } = null!;

    public virtual PromoCode? PromoCode { get; set; }
}
