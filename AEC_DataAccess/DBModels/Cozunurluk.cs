using System;
using System.Collections.Generic;

namespace AEC_DataAccess.DBModels;

public partial class Cozunurluk
{
    public int Id { get; set; }

    public string Cozunurluk1 { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public virtual Kullanici CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<Laptop> Laptops { get; set; } = new List<Laptop>();

    public virtual ICollection<Monitor> Monitors { get; set; } = new List<Monitor>();
}
