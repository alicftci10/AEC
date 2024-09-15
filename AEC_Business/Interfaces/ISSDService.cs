using AEC_DataAccess.DBModels;
using AEC_Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEC_Business.Interfaces
{
    public interface ISSDService
    {
        List<SSDDataModel> GetAllSSD();

        List<SSDDataModel> GetSSDList(string searchTerm);

        Ssd GetId(int pId);

        int Add(SSDDataModel item);

        int Update(SSDDataModel item);

        Ssd Delete(int pId);
    }
}
