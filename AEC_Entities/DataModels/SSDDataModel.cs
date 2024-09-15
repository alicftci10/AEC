using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEC_Entities.DataModels
{
    public class SSDDataModel : BaseDataModel
    {
        [Required(ErrorMessage = "Depolama Boyutu Boş Girilemez!!")]
        public int? DepolamaId { get; set; }

        [Required(ErrorMessage = "Depolama Adı Boş Girilemez!!")]
        public string DepolamaAdi { get; set; } = null!;

        public string? DepolamaIdName { get; set; }
    }
}
