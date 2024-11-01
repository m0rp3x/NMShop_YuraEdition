using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NMShop.Shared.Scaffold;

[Table("users", Schema = "NMShop")]
public partial class User
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("username")]
    [StringLength(50)]
    public string Username { get; set; } = null!;

    [Column("role")]
    [StringLength(20)]
    public string Role { get; set; } = null!;

    [InverseProperty("Client")]
    public virtual ICollection<Chat> ChatClients { get; set; } = new List<Chat>();

    [InverseProperty("Operator")]
    public virtual ICollection<Chat> ChatOperators { get; set; } = new List<Chat>();

    [InverseProperty("User")]
    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
}
