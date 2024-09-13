using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEC_Entities.DataModels
{
    public class KategoriDataModel : BaseDataModel
    {
        public string KategoriAdi { get; set; } = null!;

        public int? MainKategoriId { get; set; }

        public string? UstKategori { get; set; }

        public string? OrtaKategori { get; set; }

        public string? AltKategori { get; set; }
    }
}
