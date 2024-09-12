using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEC_Entities.DataModels
{
    public class CPUDataModel : BaseDataModel
    {
        public string IslemciSerisi { get; set; } = null!;

        public string IslemciMimarisi { get; set; } = null!;

        public string IslemciAdi { get; set; } = null!;
    }
}
