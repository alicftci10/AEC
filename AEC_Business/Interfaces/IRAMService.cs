using AEC_DataAccess.DBModels;
using AEC_Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEC_Business.Interfaces
{
    public interface IRAMService
    {
        List<RAMDataModel> GetAllRAM();

        List<RAMDataModel> GetRAMList(string searchTerm);

        Ram GetId(int pId);

        int Add(RAMDataModel item);

        int Update(RAMDataModel item);

        Ram Delete(int pId);
    }
}
