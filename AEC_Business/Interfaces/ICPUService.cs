using AEC_DataAccess.DBModels;
using AEC_Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEC_Business.Interfaces
{
    public interface ICPUService
    {
        List<CPUDataModel> GetAllCPU();

        List<CPUDataModel> GetCPUList(string searchTerm);

        Cpu GetId(int pId);

        int Add(CPUDataModel item);

        int Update(CPUDataModel item);

        Cpu Delete(int pId);
    }
}
