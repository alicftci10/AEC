using AEC_Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEC_Business.Interfaces
{
    public interface IKullaniciTuruService
    {
        List<KullaniciTuruDataModel> GetAllKullaniciTuru();
    }
}
