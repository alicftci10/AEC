using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEC_Entities.DataModels
{
    public class IsletimSistemiDataModel : BaseDataModel
    {
        [Required(ErrorMessage = "İşletim Sistemi Boş Girilemez!!")]
        public int? IsletimSistemiId { get; set; }

        [Required(ErrorMessage = "İşletim Sistemi Sürümü Boş Girilemez!!")]
        public string IsletimSistemiAdi { get; set; } = null!;

        public string? IsletimSistemiIdName { get; set; }
    }
}
