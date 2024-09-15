using AEC_DataAccess.DBModels;
using AEC_Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEC_Business.Interfaces
{
    public interface IGPUService
    {
        List<GPUDataModel> GetAllGPU();

        List<GPUDataModel> GetGPUList(string searchTerm);

        Gpu GetId(int pId);

        int Add(GPUDataModel item);

        int Update(GPUDataModel item);

        Gpu Delete(int pId);
    }
}
