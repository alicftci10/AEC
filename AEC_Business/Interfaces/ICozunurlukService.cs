using AEC_DataAccess.DBModels;
using AEC_Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEC_Business.Interfaces
{
    public interface ICozunurlukService
    {
        List<CozunurlukDataModel> GetAllCozunurluk();

        List<CozunurlukDataModel> GetCozunurlukList(string searchTerm);

        Cozunurluk GetId(int pId);

        int Add(CozunurlukDataModel item);

        int Update(CozunurlukDataModel item);

        Cozunurluk Delete(int pId);
    }
}
