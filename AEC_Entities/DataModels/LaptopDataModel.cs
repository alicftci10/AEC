using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEC_Entities.DataModels
{
    public class LaptopDataModel : BaseDataModel
    {
        [Required(ErrorMessage = "Laptop Adı Boş Girilemez!!")]
        public string LaptopAdi { get; set; } = null!;

        [Required(ErrorMessage = "Laptop Fiyatı Boş Girilemez!!")]
        public decimal? Fiyat { get; set; }

        [Required(ErrorMessage = "İşletim Sistemi Boş Girilemez!!")]
        public int? IsletimSistemiId { get; set; }

        public string? IsletimSistemiIdName { get; set; }

        [Required(ErrorMessage = "İşlemci Boş Girilemez!!")]
        public int? IslemciId { get; set; }

        public string? IslemciIdName { get; set; }

        [Required(ErrorMessage = "Ekran Kartı Boş Girilemez!!")]
        public int? EkranKartiId { get; set; }

        public string? EkranKartiIdName { get; set; }

        [Required(ErrorMessage = "Bellek Boş Girilemez!!")]
        public int? BellekId { get; set; }

        public string? BellekIdName { get; set; }

        [Required(ErrorMessage = "Depolama Boş Girilemez!!")]
        public int? DepolamaId { get; set; }

        public string? DepolamaIdName { get; set; }

        [Required(ErrorMessage = "Çözünürlük Boş Girilemez!!")]
        public int? CozunurlukId { get; set; }

        public string? CozunurlukIdName { get; set; }

        [Required(ErrorMessage = "Yenileme Hızı Boş Girilemez!!")]
        public int? YenilemeHiziId { get; set; }

        public string? YenilemeHiziIdName { get; set; }

        public string? Klavye { get; set; }

        public string? Boyut { get; set; }

        public string? Agirlik { get; set; }

        public string? Batarya { get; set; }
    }
}
