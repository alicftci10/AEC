using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEC_Entities.DataModels
{
    public class KullaniciTuruDataModel : BaseDataModel
    {
        [Required(ErrorMessage = "Tür Adı Boş Girilemez!!")]
        public string? TurAdi { get; set; }
    }
}
