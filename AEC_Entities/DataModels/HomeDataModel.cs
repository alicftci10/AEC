using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEC_Entities.DataModels
{
    public class HomeDataModel : BaseDataModel
    {
        public List<LaptopDataModel>? LaptopList { get; set; }

        public List<MonitorDataModel>? MonitorList { get; set; }

        public List<MouseDataModel>? MouseList { get; set; }
    }
}
