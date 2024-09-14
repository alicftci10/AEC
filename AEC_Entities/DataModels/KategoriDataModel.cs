using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEC_Entities.DataModels
{
    public class KategoriDataModel : BaseDataModel
    {
        [Required(ErrorMessage = "Kategori Adı Boş Girilemez!!")]
        public string KategoriAdi { get; set; } = null!;

        public int? MainKategoriId { get; set; }

        public string? UstKategori { get; set; }

        public string? OrtaKategori { get; set; }

        public string? AltKategori { get; set; }
    }
}
