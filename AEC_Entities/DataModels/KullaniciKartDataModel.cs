using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEC_Entities.DataModels
{
    public class KullaniciKartDataModel :BaseDataModel
    {
        public int KullaniciId { get; set; }

        [Required(ErrorMessage = "Kart Adı Boş Girilemez!!")]
        public string KartAdi { get; set; } = null!;

        [Required(ErrorMessage = "Kart Numarası Boş Girilemez!!")]
        [MinLength(16, ErrorMessage = "Lütfen 16 haneli Kart Numarasınızı Giriniz!!")]
        public string KartNumarasi { get; set; } = null!;

        [Required(ErrorMessage = "Kart Adı Soyadı Boş Girilemez!!")]
        public string KartAdSoyad { get; set; } = null!;

        [Required(ErrorMessage = "Kart Ay/Yıl Boş Girilemez!!")]
        public int? KartSktay { get; set; }

        [Required(ErrorMessage = "Kart Ay/Yıl Boş Girilemez!!")]
        public int? KartSktyil { get; set; }

        [Required(ErrorMessage = "Kart CVV Boş Girilemez!!")]
        public int? KartCvvkodu { get; set; }
    }
}
