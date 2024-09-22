using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEC_Entities.DataModels
{
    public class MonitorDataModel : BaseDataModel
    {
        [Required(ErrorMessage = "Monitör Adı Boş Girilemez!!")]
        public string MonitorAdi { get; set; } = null!;

        [Required(ErrorMessage = "Monitör Fiyatı Boş Girilemez!!")]
        public decimal? Fiyat { get; set; }

        [Required(ErrorMessage = "Çözünürlük Boş Girilemez!!")]
        public int? CozunurlukId { get; set; }

        public string? CozunurlukIdName { get; set; }

        [Required(ErrorMessage = "Yenileme Hızı Boş Girilemez!!")]
        public int? YenilemeHiziId { get; set; }

        public string? YenilemeHiziIdName { get; set; }

        public string? EkranYapisi { get; set; }

        public string? Hdr { get; set; }

        public string? Boyut { get; set; }

        public string? Agirlik { get; set; }

        public string? ResimUrl { get; set; }
    }
}
