using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEC_Entities.DataModels
{
    public class RAMDataModel : BaseDataModel
    {
        [Required(ErrorMessage = "Bellek Boş Girilemez!!")]
        public int? BellekId { get; set; }

        [Required(ErrorMessage = "Bellek Adı Boş Girilemez!!")]
        public string BellekAdi { get; set; } = null!;

        public string? BellekIdName { get; set; }
    }
}
