using System;
using System.Collections.Generic;

namespace AEC_DataAccess.DBModels;

public partial class Mouse
{
    public int Id { get; set; }

    public string MouseAdi { get; set; } = null!;

    public decimal Fiyat { get; set; }

    public string Renk { get; set; } = null!;

    public string Dpi { get; set; } = null!;

    public string TusSayisi { get; set; } = null!;

    public string BaglantiOzellikleri { get; set; } = null!;

    public string? Boyut { get; set; }

    public string? Agirlik { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public virtual Kullanici CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<UrunResmi> UrunResmis { get; set; } = new List<UrunResmi>();

    public virtual ICollection<UrunTakip> UrunTakips { get; set; } = new List<UrunTakip>();

    public virtual ICollection<UrunYorum> UrunYorums { get; set; } = new List<UrunYorum>();
}
