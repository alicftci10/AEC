using System;
using System.Collections.Generic;

namespace AEC_DataAccess.DBModels;

public partial class KullaniciKart
{
    public int Id { get; set; }

    public int KullaniciId { get; set; }

    public string KartAdi { get; set; } = null!;

    public string KartNumarasi { get; set; } = null!;

    public string KartAdSoyad { get; set; } = null!;

    public int KartSktay { get; set; }

    public int KartSktyil { get; set; }

    public int KartCvvkodu { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public virtual Kullanici? CreatedByNavigation { get; set; }

    public virtual Kullanici Kullanici { get; set; } = null!;
}
