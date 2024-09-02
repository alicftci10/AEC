using System;
using System.Collections.Generic;

namespace AEC_DataAccess.DBModels;

public partial class Kullanici
{
    public int Id { get; set; }

    public string Ad { get; set; } = null!;

    public string Soyad { get; set; } = null!;

    public string KullaniciAdi { get; set; } = null!;

    public string Sifre { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Telefon { get; set; }

    public string? Adres { get; set; }

    public int KullaniciTuruId { get; set; }

    public int? KartId { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public virtual ICollection<Cozunurluk> Cozunurluks { get; set; } = new List<Cozunurluk>();

    public virtual ICollection<Cpu> Cpus { get; set; } = new List<Cpu>();

    public virtual Kullanici? CreatedByNavigation { get; set; }

    public virtual ICollection<Gpu> Gpus { get; set; } = new List<Gpu>();

    public virtual ICollection<Kullanici> InverseCreatedByNavigation { get; set; } = new List<Kullanici>();

    public virtual ICollection<IsletimSistemi> IsletimSistemis { get; set; } = new List<IsletimSistemi>();

    public virtual KullaniciKart? Kart { get; set; }

    public virtual ICollection<Kategori> Kategoris { get; set; } = new List<Kategori>();

    public virtual ICollection<KullaniciKart> KullaniciKarts { get; set; } = new List<KullaniciKart>();

    public virtual KullaniciTuru KullaniciTuru { get; set; } = null!;

    public virtual ICollection<KullaniciTuru> KullaniciTurus { get; set; } = new List<KullaniciTuru>();

    public virtual ICollection<Laptop> Laptops { get; set; } = new List<Laptop>();

    public virtual ICollection<Monitor> Monitors { get; set; } = new List<Monitor>();

    public virtual ICollection<Ram> Rams { get; set; } = new List<Ram>();

    public virtual ICollection<Sepet> Sepets { get; set; } = new List<Sepet>();

    public virtual ICollection<Ssd> Ssds { get; set; } = new List<Ssd>();

    public virtual ICollection<UrunResmi> UrunResmis { get; set; } = new List<UrunResmi>();

    public virtual ICollection<UrunYorum> UrunYorums { get; set; } = new List<UrunYorum>();

    public virtual ICollection<YenilemeHizi> YenilemeHizis { get; set; } = new List<YenilemeHizi>();
}
