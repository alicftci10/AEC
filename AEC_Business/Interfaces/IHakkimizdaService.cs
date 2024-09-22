using AEC_DataAccess.DBModels;
using AEC_Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEC_Business.Interfaces
{
    public interface IHakkimizdaService
    {
        HakkimizdaDataModel GetHakkimizda();

        int Add(HakkimizdaDataModel item);

        int Update(HakkimizdaDataModel item);

        Hakkimizdum Delete(int pId);
    }
}
