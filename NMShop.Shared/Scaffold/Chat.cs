namespace NMShop.Shared.Scaffold;

public class Chat
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public User Client { get; set; }
    public int OperatorId { get; set; }
    public User Operator { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ClosedAt { get; set; }
    public bool IsOpen { get; set; }
    
    public ICollection<Message> Messages { get; set; }
}
