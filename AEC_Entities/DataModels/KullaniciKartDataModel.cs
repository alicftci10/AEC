using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEC_Entities.DataModels
{
    public class KullaniciKartDataModel :BaseDataModel
    {
        public int KullaniciId { get; set; }

        public string KartAdi { get; set; } = null!;

        public string KartNumarasi { get; set; } = null!;

        public string KartAdSoyad { get; set; } = null!;

        public int KartSktay { get; set; }

        public int KartSktyil { get; set; }

        public int KartCvvkodu { get; set; }
    }
}
