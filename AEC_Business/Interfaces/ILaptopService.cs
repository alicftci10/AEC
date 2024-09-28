using AEC_DataAccess.DBModels;
using AEC_Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEC_Business.Interfaces
{
    public interface ILaptopService
    {
        List<LaptopDataModel> GetAllLaptop();

        List<LaptopDataModel> GetLaptopList(string searchTerm);

        List<LaptopDataModel> GetLaptopList();

        LaptopDataModel GetLaptopId(int pId);

        Laptop GetId(int pId);

        int Add(LaptopDataModel item);

        int Update(LaptopDataModel item);

        Laptop Delete(int pId);
    }
}
