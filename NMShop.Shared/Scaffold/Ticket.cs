using System;
using System.Collections.Generic;

namespace NMShop.Shared.Scaffold;

public partial class Ticket
{
    public int Ticketid { get; set; }

    public int? Userid { get; set; }

    public string? Subject { get; set; }

    public string? Description { get; set; }

    public string? Status { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public virtual ICollection<Ticketmessage> Ticketmessages { get; set; } = new List<Ticketmessage>();

    public virtual User? User { get; set; }
}
