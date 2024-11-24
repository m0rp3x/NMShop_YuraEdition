using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NMShop.Shared.Scaffold;

[Table("tickets", Schema = "NMShop")]
public partial class Ticket
{
    [Key]
    [Column("ticketid")]
    public int Ticketid { get; set; }

    [Column("userid")]
    public int? Userid { get; set; }

    [Column("subject")]
    [StringLength(255)]
    public string? Subject { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [Column("status")]
    [StringLength(20)]
    public string? Status { get; set; }

    [Column("createdat")]
    public DateTime? Createdat { get; set; }

    [Column("updatedat")]
    public DateTime? Updatedat { get; set; }

    [InverseProperty("Ticket")]
    
    [Display(AutoGenerateField = false)]
    public virtual ICollection<Ticketmessage> Ticketmessages { get; set; } = new List<Ticketmessage>();

    [ForeignKey("Userid")]
    [InverseProperty("Tickets")]
    [Display(AutoGenerateField = false)]
    public virtual User? User { get; set; }
    
    public override string ToString()
    {
        return Subject ?? base.ToString() + " ticket" + Description;
    }
}
