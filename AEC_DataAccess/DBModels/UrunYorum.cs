using System;
using System.Collections.Generic;

namespace AEC_DataAccess.DBModels;

public partial class UrunYorum
{
    public int Id { get; set; }

    public int? LaptopId { get; set; }

    public int? MonitorId { get; set; }

    public int? MouseId { get; set; }

    public string Yorum { get; set; } = null!;

    public short? YorumPuan { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public virtual Kullanici CreatedByNavigation { get; set; } = null!;

    public virtual Laptop? Laptop { get; set; }

    public virtual Monitor? Monitor { get; set; }

    public virtual Mouse? Mouse { get; set; }
}
