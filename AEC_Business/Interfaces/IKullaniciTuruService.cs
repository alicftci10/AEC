using AEC_DataAccess.DBModels;
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

        List<KullaniciTuruDataModel> GetKullaniciTuruList(string searchTerm);

        KullaniciTuru GetId(int pId);

        int Add(KullaniciTuruDataModel item);

        int Update(KullaniciTuruDataModel item);

        KullaniciTuru Delete(int pId);
    }
}
