﻿using System;
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

    public DateTime CreatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public virtual ICollection<Cozunurluk> Cozunurluks { get; set; } = new List<Cozunurluk>();

    public virtual ICollection<Cpu> Cpus { get; set; } = new List<Cpu>();

    public virtual ICollection<Gpu> Gpus { get; set; } = new List<Gpu>();

    public virtual ICollection<Hakkimizdum> Hakkimizda { get; set; } = new List<Hakkimizdum>();

    public virtual ICollection<IsletimSistemi> IsletimSistemis { get; set; } = new List<IsletimSistemi>();

    public virtual ICollection<Kategori> Kategoris { get; set; } = new List<Kategori>();

    public virtual ICollection<KullaniciKart> KullaniciKartCreatedByNavigations { get; set; } = new List<KullaniciKart>();

    public virtual ICollection<KullaniciKart> KullaniciKartKullanicis { get; set; } = new List<KullaniciKart>();

    public virtual KullaniciTuru KullaniciTuru { get; set; } = null!;

    public virtual ICollection<KullaniciTuru> KullaniciTurus { get; set; } = new List<KullaniciTuru>();

    public virtual ICollection<Laptop> Laptops { get; set; } = new List<Laptop>();

    public virtual ICollection<Mouse> Mice { get; set; } = new List<Mouse>();

    public virtual ICollection<Monitor> Monitors { get; set; } = new List<Monitor>();

    public virtual ICollection<Ram> Rams { get; set; } = new List<Ram>();

    public virtual ICollection<Ssd> Ssds { get; set; } = new List<Ssd>();

    public virtual ICollection<UrunResmi> UrunResmis { get; set; } = new List<UrunResmi>();

    public virtual ICollection<UrunTakip> UrunTakipCreatedByNavigations { get; set; } = new List<UrunTakip>();

    public virtual ICollection<UrunTakip> UrunTakipUpdatedByNavigations { get; set; } = new List<UrunTakip>();

    public virtual ICollection<UrunYorum> UrunYorums { get; set; } = new List<UrunYorum>();

    public virtual ICollection<YenilemeHizi> YenilemeHizis { get; set; } = new List<YenilemeHizi>();
}
