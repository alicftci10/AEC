using System;
using System.Collections.Generic;

namespace AEC_DataAccess.DBModels;

public partial class Laptop
{
    public int Id { get; set; }

    public string LaptopAdi { get; set; } = null!;

    public decimal Fiyat { get; set; }

    public int IsletimSistemiId { get; set; }

    public int Cpuid { get; set; }

    public int Gpuid { get; set; }

    public int Ramid { get; set; }

    public int Ssdid { get; set; }

    public int CozunurlukId { get; set; }

    public int YenilemeHiziId { get; set; }

    public string? Klavye { get; set; }

    public string? Boyut { get; set; }

    public string? Agirlik { get; set; }

    public string? Batarya { get; set; }

    public int UrunKodu { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public virtual Cozunurluk Cozunurluk { get; set; } = null!;

    public virtual Cpu Cpu { get; set; } = null!;

    public virtual Kullanici CreatedByNavigation { get; set; } = null!;

    public virtual Gpu Gpu { get; set; } = null!;

    public virtual IsletimSistemi IsletimSistemi { get; set; } = null!;

    public virtual Ram Ram { get; set; } = null!;

    public virtual ICollection<Sepet> Sepets { get; set; } = new List<Sepet>();

    public virtual Ssd Ssd { get; set; } = null!;

    public virtual ICollection<UrunResmi> UrunResmis { get; set; } = new List<UrunResmi>();

    public virtual ICollection<UrunYorum> UrunYorums { get; set; } = new List<UrunYorum>();

    public virtual YenilemeHizi YenilemeHizi { get; set; } = null!;
}
