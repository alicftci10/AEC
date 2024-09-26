using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEC_Entities.DataModels
{
    public class UrunResmiDataModel : BaseDataModel
    {
        public int? LaptopId { get; set; }

        public int? MonitorId { get; set; }

        public int? MouseId { get; set; }

        [Required(ErrorMessage = "Resim Url Bilgisi Boş Girilemez!!")]
        public string ResimUrl { get; set; } = null!;

        public decimal? ResimBoyutu { get; set; }

        public string ResimTuru { get; set; } = null!;
    }
}
