using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEC_Entities.DataModels
{
    public class KullaniciDataModel : BaseDataModel
    {
        [Required(ErrorMessage = "Ad Boş Girilemez!!")]
        public string Ad { get; set; } = null!;

        [Required(ErrorMessage = "Soyad Boş Girilemez!!")]
        public string Soyad { get; set; } = null!;

        [Required(ErrorMessage = "Kullanıcı Adı Boş Girilemez!!")]
        public string KullaniciAdi { get; set; } = null!;

        [Required(ErrorMessage = "Şifre Boş Girilemez!!")]
        public string Sifre { get; set; } = null!;

        [Required(ErrorMessage = "Email Boş Girilemez!!")]
        public string Email { get; set; } = null!;

        public string? Telefon { get; set; }

        public string? Adres { get; set; }

        [Required(ErrorMessage = "Kullanıcı Türü Boş Girilemez!!")]
        public int KullaniciTuruId { get; set; }

        public int? KartId { get; set; }

        [Required(ErrorMessage = "Kullanıcı Adı Boş Girilemez!!")]
        public string? EmailorKullaniciAdi { get; set; }

        public string? CreatedByName { get; set; }

        public string? ErrorMessage { get; set; }

        public bool IsSuccess { get; set; }

        public string? JwtToken { get; set; }
    }
}