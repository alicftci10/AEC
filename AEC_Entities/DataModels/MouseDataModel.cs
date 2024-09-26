using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEC_Entities.DataModels
{
    public class MouseDataModel : BaseDataModel
    {
        [Required(ErrorMessage = "Mouse Adı Boş Girilemez!!")]
        public string MouseAdi { get; set; } = null!;

        [Required(ErrorMessage = "Mouse Fiyatı Boş Girilemez!!")]
        public decimal? Fiyat { get; set; }

        [Required(ErrorMessage = "Mouse Rengi Boş Girilemez!!")]
        public string Renk { get; set; } = null!;

        [Required(ErrorMessage = "Mouse DPI Boş Girilemez!!")]
        public string Dpi { get; set; } = null!;

        [Required(ErrorMessage = "Mouse Tuş Sayısı Boş Girilemez!!")]
        public string TusSayisi { get; set; } = null!;

        [Required(ErrorMessage = "Mouse Bağlantı Özellikleri Boş Girilemez!!")]
        public string BaglantiOzellikleri { get; set; } = null!;

        public string? Boyut { get; set; }

        public string? Agirlik { get; set; }

        public string? ResimUrl { get; set; }
    }
}
