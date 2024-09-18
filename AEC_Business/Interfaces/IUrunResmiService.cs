using AEC_DataAccess.DBModels;
using AEC_Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEC_Business.Interfaces
{
    public interface IUrunResmiService
    {
        List<UrunResmiDataModel> GetAllUrunResmi();

        List<UrunResmiDataModel> GetLaptopResmiList(int LaptopId);

        List<UrunResmiDataModel> GetMonitorResmiList(int MonitorId);

        UrunResmi GetId(int pId);

        int Add(UrunResmiDataModel item);

        int Update(UrunResmiDataModel item);

        UrunResmi Delete(int pId);
    }
}
