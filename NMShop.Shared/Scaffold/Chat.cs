using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NMShop.Shared.Scaffold;

[Table("chats", Schema = "NMShop")]
public partial class Chat
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("clientid")]
    public int Clientid { get; set; }

    [Column("operatorid")]
    public int? Operatorid { get; set; }

    [Column("createdat", TypeName = "timestamp without time zone")]
    public DateTime Createdat { get; set; }

    [Column("closedat", TypeName = "timestamp without time zone")]
    public DateTime? Closedat { get; set; }

    [Column("isopen")]
    public bool Isopen { get; set; }

    [ForeignKey("Clientid")]
    [InverseProperty("ChatClients")]
    public virtual User Client { get; set; } = null!;

    [InverseProperty("Chat")]
    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    [ForeignKey("Operatorid")]
    [InverseProperty("ChatOperators")]
    public virtual User? Operator { get; set; }
}
