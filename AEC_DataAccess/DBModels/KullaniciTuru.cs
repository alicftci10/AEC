using System;
using System.Collections.Generic;

namespace AEC_DataAccess.DBModels;

public partial class KullaniciTuru
{
    public int Id { get; set; }

    public string TurAdi { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public virtual Kullanici CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<Kullanici> Kullanicis { get; set; } = new List<Kullanici>();
}
