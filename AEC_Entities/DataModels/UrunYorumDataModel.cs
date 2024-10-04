using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEC_Entities.DataModels
{
    public class UrunYorumDataModel : BaseDataModel
    {
        public int? LaptopId { get; set; }

        public int? MonitorId { get; set; }

        public int? MouseId { get; set; }

        public string? Yorum { get; set; }

        public short? YorumPuan { get; set; }
    }
}
