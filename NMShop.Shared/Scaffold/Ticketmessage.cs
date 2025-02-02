using System;
using System.Collections.Generic;

namespace NMShop.Shared.Scaffold;

public partial class Ticketmessage
{
    public int Messageid { get; set; }

    public int? Ticketid { get; set; }

    public int? Userid { get; set; }

    public string? Message { get; set; }

    public DateTime? Sentat { get; set; }

    public virtual Ticket? Ticket { get; set; }

    public virtual User? User { get; set; }
}
