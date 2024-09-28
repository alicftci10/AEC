using AEC_DataAccess.DBModels;
using AEC_Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEC_Business.Interfaces
{
    public interface IKategoriService
    {
        List<KategoriDataModel> GetAllKategori();

        List<KategoriDataModel> GetKategoriList(string searchTerm);

        List<KategoriDataModel> GetKategoriList();

        Kategori GetId(int pId);

        int Add(KategoriDataModel item);

        int Update(KategoriDataModel item);

        Kategori Delete(int pId);
    }
}
