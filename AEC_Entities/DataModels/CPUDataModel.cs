using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEC_Entities.DataModels
{
    public class CPUDataModel : BaseDataModel
    {
        [Required(ErrorMessage = "İşlemci Serisi Boş Girilemez!!")]
        public int? IslemciSerisiId { get; set; }

        [Required(ErrorMessage = "İşlemci Mimarisi Boş Girilemez!!")]
        public string IslemciMimarisi { get; set; } = null!;

        [Required(ErrorMessage = "İşlemci Adı Boş Girilemez!!")]
        public string IslemciAdi { get; set; } = null!;

        public string? IslemciSerisiName { get; set; }
    }
}
