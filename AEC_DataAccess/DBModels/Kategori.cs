using System;
using System.Collections.Generic;

namespace AEC_DataAccess.DBModels;

public partial class Kategori
{
    public int Id { get; set; }

    public string KategoriAdi { get; set; } = null!;

    public int? MainKategoriId { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public virtual ICollection<Cozunurluk> Cozunurluks { get; set; } = new List<Cozunurluk>();

    public virtual ICollection<Cpu> Cpus { get; set; } = new List<Cpu>();

    public virtual Kullanici CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<Gpu> Gpus { get; set; } = new List<Gpu>();

    public virtual ICollection<Kategori> InverseMainKategori { get; set; } = new List<Kategori>();

    public virtual ICollection<IsletimSistemi> IsletimSistemis { get; set; } = new List<IsletimSistemi>();

    public virtual Kategori? MainKategori { get; set; }

    public virtual ICollection<Ram> Rams { get; set; } = new List<Ram>();

    public virtual ICollection<Ssd> Ssds { get; set; } = new List<Ssd>();

    public virtual ICollection<YenilemeHizi> YenilemeHizis { get; set; } = new List<YenilemeHizi>();
}
