using AEC_DataAccess.DBModels;
using AEC_Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEC_Business.Interfaces
{
    public interface IMouseService
    {
        List<MouseDataModel> GetAllMouse();

        List<MouseDataModel> GetMouseList(string searchTerm);

        List<MouseDataModel> GetMouseList();

        MouseDataModel GetMouseId(int pId);

        Mouse GetId(int pId);

        int Add(MouseDataModel item);

        int Update(MouseDataModel item);

        Mouse Delete(int pId);
    }
}
