using AEC_DataAccess.DBModels;
using AEC_DataAccess.GenericRepository.Interfaces;
using AEC_Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEC_DataAccess.EFInterfaces
{
    public interface IEFMouseRepository : IRepository<Mouse>
    {
        List<MouseDataModel> GetMouseList(string searchTerm);

        MouseDataModel GetMouseId(int pId);
    }
}
