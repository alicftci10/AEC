using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEC_Entities.DataModels
{
    public class YenilemeHiziDataModel : BaseDataModel
    {
        [Required(ErrorMessage = "Yenileme Hızı Değeri Boş Girilemez!!")]
        public int? YenilemeHiziId { get; set; }

        [Required(ErrorMessage = "Yenileme Hızı Adı Boş Girilemez!!")]
        public string YenilemeHiziAdi { get; set; } = null!;

        public string? YenilemeHiziIdName { get; set; }
    }
}
