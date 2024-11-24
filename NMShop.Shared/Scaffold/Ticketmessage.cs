using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NMShop.Shared.Scaffold;

[Table("ticketmessages", Schema = "NMShop")]
public partial class Ticketmessage
{
    [Key]
    [Column("messageid")]
    public int Messageid { get; set; }

    [Column("ticketid")]
    public int? Ticketid { get; set; }

    [Column("userid")]
    public int? Userid { get; set; }

    [Column("message")]
    public string? Message { get; set; }

    [Column("sentat")]
    public DateTime? Sentat { get; set; }

    [ForeignKey("Ticketid")]
    [InverseProperty("Ticketmessages")]
    [Display(AutoGenerateField = false)]
    public virtual Ticket? Ticket { get; set; }

    [ForeignKey("Userid")]
    [InverseProperty("Ticketmessages")]
    [Display(AutoGenerateField = false)]
    public virtual User? User { get; set; }
    
    public override string ToString()
    {
        return Message;
    }
}
