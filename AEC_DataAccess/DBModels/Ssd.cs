using System;
using System.Collections.Generic;

namespace AEC_DataAccess.DBModels;

public partial class Ssd
{
    public int Id { get; set; }

    public string Ssdadi { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public virtual Kullanici CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<Laptop> Laptops { get; set; } = new List<Laptop>();
}
