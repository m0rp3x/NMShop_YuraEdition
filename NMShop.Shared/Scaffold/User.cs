using System;
using System.Collections.Generic;

namespace NMShop.Shared.Scaffold;

public partial class User
{
    public int Userid { get; set; }

    public string? Username { get; set; }

    public long? Telegramid { get; set; }

    public string? Role { get; set; }

    public DateTime? Createdat { get; set; }

    public virtual ICollection<Ticketmessage> Ticketmessages { get; set; } = new List<Ticketmessage>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
