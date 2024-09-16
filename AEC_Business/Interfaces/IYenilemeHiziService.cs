using AEC_DataAccess.DBModels;
using AEC_Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEC_Business.Interfaces
{
    public interface IYenilemeHiziService
    {
        List<YenilemeHiziDataModel> GetAllYenilemeHizi();

        List<YenilemeHiziDataModel> GetYenilemeHiziList(string searchTerm);

        YenilemeHizi GetId(int pId);

        int Add(YenilemeHiziDataModel item);

        int Update(YenilemeHiziDataModel item);

        YenilemeHizi Delete(int pId);
    }
}
