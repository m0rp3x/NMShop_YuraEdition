using System.ComponentModel.DataAnnotations;

namespace NMShop.Shared.Models;

public class TicketReplyModel
{
    [Required]
    public int TicketId { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public string Message { get; set; }
}
