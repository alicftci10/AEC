using AEC_Business.Interfaces;
using AEC_DataAccess.DBModels;
using AEC_DataAccess.EFInterfaces;
using AEC_Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEC_Business.Managers
{
    public class CozunurlukManager : BaseManager, ICozunurlukService
    {
        private readonly IEFCozunurlukRepository _CozunurlukRepository;
        public CozunurlukManager(IEFCozunurlukRepository CozunurlukRepository)
        {
            _CozunurlukRepository = CozunurlukRepository;
        }

        public List<CozunurlukDataModel> GetAllCozunurluk()
        {
            List<CozunurlukDataModel> listCozunurluk = new List<CozunurlukDataModel>();

            List<Cozunurluk> list = _CozunurlukRepository.GetAll() as List<Cozunurluk>;

            if (list != null)
            {
                foreach (Cozunurluk item in list)
                {
                    CozunurlukDataModel model = new CozunurlukDataModel();
                    model.Id = item.Id;
                    model.CozunurlukId = item.CozunurlukId;
                    model.CozunurlukAdi = item.CozunurlukAdi;
                    model.CreatedAt = item.CreatedAt;
                    model.CreatedBy = item.CreatedBy;

                    listCozunurluk.Add(model);
                }
            }

            return listCozunurluk;
        }

        private Cozunurluk GetDataModel(CozunurlukDataModel model)
        {
            Cozunurluk item = new Cozunurluk();

            item.CozunurlukId = model.CozunurlukId.Value;
            item.CozunurlukAdi = model.CozunurlukAdi;
            item.CreatedAt = DateTime.Now;
            item.CreatedBy = model.CreatedBy.Value;

            if (model.Id > 0)
            {
                item.Id = model.Id;
            }

            return item;
        }

        public List<CozunurlukDataModel> GetCozunurlukList(string searchTerm)
        {
            return _CozunurlukRepository.GetCozunurlukList(searchTerm);
        }

        public Cozunurluk GetId(int pId)
        {
            return _CozunurlukRepository.GetSelect(pId);
        }

        public int Add(CozunurlukDataModel item)
        {
            return _CozunurlukRepository.Add(GetDataModel(item)).Id;
        }

        public int Update(CozunurlukDataModel item)
        {
            return _CozunurlukRepository.Update(GetDataModel(item)).Id;
        }

        public Cozunurluk Delete(int pId)
        {
            return _CozunurlukRepository.Delete(pId);
        }
    }
}
