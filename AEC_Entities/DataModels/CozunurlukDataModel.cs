using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEC_Entities.DataModels
{
    public class CozunurlukDataModel : BaseDataModel
    {
        [Required(ErrorMessage = "Çözünürlük Değeri Boş Girilemez!!")]
        public int? CozunurlukId { get; set; }

        [Required(ErrorMessage = "Çözünürlük Adı Boş Girilemez!!")]
        public string CozunurlukAdi { get; set; } = null!;

        public string? CozunurlukIdName { get; set; }
    }
}
