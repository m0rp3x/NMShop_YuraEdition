namespace NMShop.Shared.Models;

public class OrderDto
{
    public int Id { get; set; }
    public string ClientFullName { get; set; }
    public string DeliveryAdress { get; set; }
    public string DeliveryTypeName { get; set; }
    public string PaymentTypeName { get; set; }
    public string OrderStatusName { get; set; }
    public string ContactValue { get; set; }
    
}
