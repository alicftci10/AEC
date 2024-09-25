using System;
using System.Collections.Generic;

namespace AEC_DataAccess.DBModels;

public partial class Laptop
{
    public int Id { get; set; }

    public string LaptopAdi { get; set; } = null!;

    public decimal Fiyat { get; set; }

    public int IsletimSistemiId { get; set; }

    public int IslemciId { get; set; }

    public int EkranKartiId { get; set; }

    public int BellekId { get; set; }

    public int DepolamaId { get; set; }

    public int CozunurlukId { get; set; }

    public int YenilemeHiziId { get; set; }

    public string? Klavye { get; set; }

    public string? Boyut { get; set; }

    public string? Agirlik { get; set; }

    public string? Batarya { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public virtual Ram Bellek { get; set; } = null!;

    public virtual Cozunurluk Cozunurluk { get; set; } = null!;

    public virtual Kullanici CreatedByNavigation { get; set; } = null!;

    public virtual Ssd Depolama { get; set; } = null!;

    public virtual Gpu EkranKarti { get; set; } = null!;

    public virtual Cpu Islemci { get; set; } = null!;

    public virtual IsletimSistemi IsletimSistemi { get; set; } = null!;

    public virtual ICollection<UrunDurum> UrunDurums { get; set; } = new List<UrunDurum>();

    public virtual ICollection<UrunResmi> UrunResmis { get; set; } = new List<UrunResmi>();

    public virtual ICollection<UrunYorum> UrunYorums { get; set; } = new List<UrunYorum>();

    public virtual YenilemeHizi YenilemeHizi { get; set; } = null!;
}
