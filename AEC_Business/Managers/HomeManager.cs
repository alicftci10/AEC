using AEC_Business.Interfaces;
using AEC_DataAccess.EFOperations;
using AEC_Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEC_Business.Managers
{
    public class HomeManager : IHomeService
    {
        public HomeManager() { }

        public HomeDataModel GetHomeSearchList(int pId)
        {
            return new EFHome().GetHomeSearchList(pId);
        }
    }
}
