using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEC_Entities.DataModels
{
    public class BaseDataModel
    {
        public int Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? CreatedBy { get; set; }

        public bool IsSuccess { get; set; }

        public string? CreatedByName { get; set; }

        public int? Count { get; set; }

        public short? OrtalamaYorumPuan { get; set; }
    }
}
