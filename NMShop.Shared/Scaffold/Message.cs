using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NMShop.Shared.Scaffold;

[Table("messages", Schema = "NMShop")]
public partial class Message
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("chatid")]
    public int Chatid { get; set; }

    [Column("userid")]
    public int Userid { get; set; }

    [Column("content")]
    public string Content { get; set; } = null!;

    [Column("timestamp", TypeName = "timestamp without time zone")]
    public DateTime Timestamp { get; set; }

    [ForeignKey("Chatid")]
    [InverseProperty("Messages")]
    public virtual Chat Chat { get; set; } = null!;

    [ForeignKey("Userid")]
    [InverseProperty("Messages")]
    public virtual User User { get; set; } = null!;
}
