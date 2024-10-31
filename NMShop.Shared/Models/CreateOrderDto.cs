using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMShop.Shared.Models
{
    public class CreateOrderDto
    {
        public string ClientFullName { get; set; } = null!;
        public string DeliveryAdress { get; set; } = null!;
        public int DeliveryTypeId { get; set; }
        public int PaymentTypeId { get; set; }
        public int ContactMethodId { get; set; }
        public string ContactValue { get; set; } = null!;
        public string? DeliveryRecipientFullName { get; set; }
        public string? DeliveryRecipientPhone { get; set; }
        public string? Comment { get; set; }
        public int OrderStatusId { get; set; }
        public string? EstimatedDeliveryDateRange { get; set; }
        public List<OrderPartDto> OrderParts { get; set; } = new();
        public string? PromoCode { get; set; }
    }

    public class OrderPartDto
    {
        public int StockInfoId { get; set; }
        public int Amount { get; set; }
    }

}
