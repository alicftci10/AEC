using System;
using System.Collections.Generic;

namespace AEC_DataAccess.DBModels;

public partial class UrunResmi
{
    public int Id { get; set; }

    public int? LaptopId { get; set; }

    public int? MonitorId { get; set; }

    public string ResimUrl { get; set; } = null!;

    public decimal ResimBoyutu { get; set; }

    public string ResimTuru { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public virtual Kullanici CreatedByNavigation { get; set; } = null!;

    public virtual Laptop? Laptop { get; set; }

    public virtual Monitor? Monitor { get; set; }
}
