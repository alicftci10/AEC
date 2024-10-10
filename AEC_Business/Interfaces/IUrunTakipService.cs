using AEC_DataAccess.DBModels;
using AEC_Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEC_Business.Interfaces
{
    public interface IUrunTakipService
    {
        List<UrunTakipDataModel> GetAllUrunTakip();

        List<UrunTakipDataModel> BekliyorList(string searchTerm);

        List<UrunTakipDataModel> TamamlandiList(string searchTerm);

        List<UrunTakipDataModel> IptalList(string searchTerm);

        List<UrunTakipDataModel> GetFavoriList(int pKullaniciId);

        List<UrunTakipDataModel> GetSepetList(int pKullaniciId);

        List<UrunTakipDataModel> GetSiparisList(int pKullaniciId);

        UrunTakip AllSepeteEkle(UrunTakipDataModel item);

        UrunTakipDataModel GetDataModel(int pId);

        UrunTakip GetId(int pId);

        UrunTakip AddFavori(UrunTakipDataModel item);

        UrunTakip AddSepet(UrunTakipDataModel item);

        int Add(UrunTakipDataModel item);

        int Update(UrunTakipDataModel item);

        UrunTakip AllSepetDelete(UrunTakipDataModel item);

        UrunTakip Delete(int pId);
    }
}
