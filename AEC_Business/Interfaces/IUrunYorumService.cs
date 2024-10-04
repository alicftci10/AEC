using AEC_DataAccess.DBModels;
using AEC_Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEC_Business.Interfaces
{
    public interface IUrunYorumService
    {
        List<UrunYorumDataModel> GetAllUrunYorum();

        List<UrunYorumDataModel> GetUrunYorumLaptopList(int pLaptopId);

        List<UrunYorumDataModel> GetUrunYorumMonitorList(int pMonitorId);

        List<UrunYorumDataModel> GetUrunYorumMouseList(int pMouseId);

        UrunYorum GetId(int pId);

        int Add(UrunYorumDataModel item);

        int Update(UrunYorumDataModel item);

        UrunYorum Delete(int pId);
    }
}
