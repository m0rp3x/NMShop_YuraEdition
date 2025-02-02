using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NMShop.Shared.Scaffold;

public partial class CustomOrder
{
    [Key]
    public int Id { get; set; }

    public string? UserName { get; set; }

    public string? UserPhone { get; set; }

    public string? ProductDescription { get; set; }

    public DateTime? CreatedAt { get; set; }
}
