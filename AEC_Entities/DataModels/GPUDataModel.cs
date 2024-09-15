using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEC_Entities.DataModels
{
    public class GPUDataModel : BaseDataModel
    {
        [Required(ErrorMessage = "Ekran Kartı Serisi Boş Girilemez!!")]
        public int? EkranKartiSerisiId { get; set; }

        [Required(ErrorMessage = "Ekran Kartı Adı Boş Girilemez!!")]
        public string EkranKartiAdi { get; set; } = null!;

        public string? EkranKartiSerisiName { get; set; }
    }
}
