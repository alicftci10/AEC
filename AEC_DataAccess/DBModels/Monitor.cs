using System;
using System.Collections.Generic;

namespace AEC_DataAccess.DBModels;

public partial class Monitor
{
    public int Id { get; set; }

    public string MonitorAdi { get; set; } = null!;

    public decimal Fiyat { get; set; }

    public int CozunurlukId { get; set; }

    public int YenilemeHiziId { get; set; }

    public string? EkranYapisi { get; set; }

    public string? Hdr { get; set; }

    public string? Boyut { get; set; }

    public string? Agirlik { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public virtual Cozunurluk Cozunurluk { get; set; } = null!;

    public virtual Kullanici CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<Sepet> Sepets { get; set; } = new List<Sepet>();

    public virtual ICollection<UrunResmi> UrunResmis { get; set; } = new List<UrunResmi>();

    public virtual ICollection<UrunYorum> UrunYorums { get; set; } = new List<UrunYorum>();

    public virtual YenilemeHizi YenilemeHizi { get; set; } = null!;
}
