using AEC_DataAccess.DBModels;
using AEC_DataAccess.GenericRepository.Interfaces;
using AEC_Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEC_DataAccess.EFInterfaces
{
    public interface IEFUrunTakipRepository : IRepository<UrunTakip>
    {
        List<UrunTakipDataModel> BekliyorList(string searchTerm);

        List<UrunTakipDataModel> TamamlandiList(string searchTerm);

        List<UrunTakipDataModel> IptalList(string searchTerm);

        List<UrunTakipDataModel> GetFavoriList(int pKullaniciId);

        List<UrunTakipDataModel> GetSepetList(int pKullaniciId);

        UrunTakipDataModel GetSepetFavoriDurum(UrunTakipDataModel model);
    }
}
