using System;
using System.Collections.Generic;

namespace AEC_DataAccess.DBModels;

public partial class Ram
{
    public int Id { get; set; }

    public int BellekId { get; set; }

    public string BellekAdi { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public virtual Kategori Bellek { get; set; } = null!;

    public virtual Kullanici CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<Laptop> Laptops { get; set; } = new List<Laptop>();
}
