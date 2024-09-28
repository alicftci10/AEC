using AEC_DataAccess.DBModels;
using AEC_DataAccess.GenericRepository.Interfaces;
using AEC_Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monitor = AEC_DataAccess.DBModels.Monitor;

namespace AEC_DataAccess.EFInterfaces
{
    public interface IEFMonitorRepository : IRepository<Monitor>
    {
        List<MonitorDataModel> GetMonitorList(string searchTerm);

        List<MonitorDataModel> GetMonitorList();

        MonitorDataModel GetMonitorId(int pId);
    }
}
