using AEC_DataAccess.DBModels;
using AEC_Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monitor = AEC_DataAccess.DBModels.Monitor;

namespace AEC_Business.Interfaces
{
    public interface IMonitorService
    {
        List<MonitorDataModel> GetAllMonitor();

        List<MonitorDataModel> GetMonitorList(string searchTerm);

        List<MonitorDataModel> GetMonitorList();

        MonitorDataModel GetMonitorId(int pId);

        Monitor GetId(int pId);

        int Add(MonitorDataModel item);

        int Update(MonitorDataModel item);

        Monitor Delete(int pId);
    }
}
