using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NMShop.Shared.Scaffold;

[Table("tg_admins", Schema = "NMShop")]
[Index("Username", Name = "tg_admins_username_uindex", IsUnique = true)]
public partial class TgAdmin
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("username")]
    [StringLength(255)]
    public string Username { get; set; } = null!;

    [Column("telegramid")]
    [StringLength(12)]
    public string Telegramid { get; set; } = null!;
}
