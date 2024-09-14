using System;
using System.Collections.Generic;

namespace AEC_DataAccess.DBModels;

public partial class Gpu
{
    public int Id { get; set; }

    public int EkranKartiSerisiId { get; set; }

    public string EkranKartiAdi { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public virtual Kullanici CreatedByNavigation { get; set; } = null!;

    public virtual Kategori EkranKartiSerisi { get; set; } = null!;

    public virtual ICollection<Laptop> Laptops { get; set; } = new List<Laptop>();
}
