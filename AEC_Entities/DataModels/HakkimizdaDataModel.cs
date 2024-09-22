using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEC_Entities.DataModels
{
    public class HakkimizdaDataModel : BaseDataModel
    {
        public string? Vizyon { get; set; }

        public string? Misyon { get; set; }

        public string? Hikaye { get; set; }

        public string? Telefon { get; set; }

        public string? Adres { get; set; }

        public string? Email { get; set; }

        public string? CalismaGunleri { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int? UpdatedBy { get; set; }
    }
}
