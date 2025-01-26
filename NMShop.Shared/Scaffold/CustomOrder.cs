namespace NMShop.Shared.Scaffold;

public class CustomOrder
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string UserPhone { get; set; }
    public string ProductDescription { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // По умолчанию UTC
}
