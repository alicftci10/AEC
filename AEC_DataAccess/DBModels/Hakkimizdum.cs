using System;
using System.Collections.Generic;

namespace AEC_DataAccess.DBModels;

public partial class Hakkimizdum
{
    public int Id { get; set; }

    public string? Vizyon { get; set; }

    public string? Misyon { get; set; }

    public string? Hikaye { get; set; }

    public string? Telefon { get; set; }

    public string? Adres { get; set; }

    public string? Email { get; set; }

    public string? CalismaGunleri { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int UpdatedBy { get; set; }

    public virtual Kullanici UpdatedByNavigation { get; set; } = null!;
}
