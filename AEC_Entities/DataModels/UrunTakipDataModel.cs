﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEC_Entities.DataModels
{
    public class UrunTakipDataModel : BaseDataModel
    {
        public int? LaptopId { get; set; }

        public int? MonitorId { get; set; }

        public int? MouseId { get; set; }

        public int? Adet { get; set; }

        public bool? Favori { get; set; }

        public bool? SepetDurum { get; set; }

        public int? SiparisDurum { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int? UpdatedBy { get; set; }

        public string? UpdatedByName { get; set; }

        public string? UrunAdi { get; set; }

        public decimal? Fiyat { get; set; }

        public string? ResimUrl { get; set; }
    }
}
