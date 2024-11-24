using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NMShop.Shared.Scaffold;

[Table("users", Schema = "NMShop")]
[Index("Telegramid", Name = "users_telegramid_key", IsUnique = true)]
public partial class User
{
    [Key]
    [Column("userid")]
    public int Userid { get; set; }

    [Column("username")]
    [StringLength(50)]
    public string? Username { get; set; }

    [Column("telegramid")]
    public long? Telegramid { get; set; }

    [Column("role")]
    [StringLength(20)]
    public string? Role { get; set; }

    [Column("createdat")]
    public DateTime? Createdat { get; set; }

    [InverseProperty("User")]
    [Display(AutoGenerateField = false)]
    public virtual ICollection<Ticketmessage> Ticketmessages { get; set; } = new List<Ticketmessage>();

    [InverseProperty("User")]
    
    [Display(AutoGenerateField = false)]
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
