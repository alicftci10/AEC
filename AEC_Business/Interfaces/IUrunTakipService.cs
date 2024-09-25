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

        List<UrunTakipDataModel> GetUrunTakipList(string searchTerm);

        UrunTakip GetId(int pId);

        int Add(UrunTakipDataModel item);

        int Update(UrunTakipDataModel item);

        UrunTakip Delete(int pId);
    }
}
