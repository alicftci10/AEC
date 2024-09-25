using System;
using System.Collections.Generic;

namespace AEC_DataAccess.DBModels;

public partial class UrunTakip
{
    public int Id { get; set; }

    public int? LaptopId { get; set; }

    public int? MonitorId { get; set; }

    public int? Adet { get; set; }

    public bool? Favori { get; set; }

    public bool? SepetDurum { get; set; }

    public int? SiparisDurum { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual Kullanici CreatedByNavigation { get; set; } = null!;

    public virtual Laptop? Laptop { get; set; }

    public virtual Monitor? Monitor { get; set; }

    public virtual Kullanici? UpdatedByNavigation { get; set; }
}
