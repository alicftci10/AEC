using System;
using System.Collections.Generic;

namespace AEC_DataAccess.DBModels;

public partial class Kategori
{
    public int Id { get; set; }

    public string KategoriAdi { get; set; } = null!;

    public int? AnaKategoriId { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public virtual Kategori? AnaKategori { get; set; }

    public virtual Kullanici CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<Kategori> InverseAnaKategori { get; set; } = new List<Kategori>();

    public virtual ICollection<Laptop> Laptops { get; set; } = new List<Laptop>();

    public virtual ICollection<Monitor> Monitors { get; set; } = new List<Monitor>();
}
