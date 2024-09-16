using AEC_DataAccess.DBModels;
using AEC_Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEC_Business.Interfaces
{
    public interface IIsletimSistemiService
    {
        List<IsletimSistemiDataModel> GetAllIsletimSistemi();

        List<IsletimSistemiDataModel> GetIsletimSistemiList(string searchTerm);

        IsletimSistemi GetId(int pId);

        int Add(IsletimSistemiDataModel item);

        int Update(IsletimSistemiDataModel item);

        IsletimSistemi Delete(int pId);
    }
}
