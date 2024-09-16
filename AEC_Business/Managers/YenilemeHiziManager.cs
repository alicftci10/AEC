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
    public class YenilemeHiziManager : BaseManager, IYenilemeHiziService
    {
        private readonly IEFYenilemeHiziRepository _YenilemeHiziRepository;
        public YenilemeHiziManager(IEFYenilemeHiziRepository YenilemeHiziRepository)
        {
            _YenilemeHiziRepository = YenilemeHiziRepository;
        }

        public List<YenilemeHiziDataModel> GetAllYenilemeHizi()
        {
            List<YenilemeHiziDataModel> listYenilemeHizi = new List<YenilemeHiziDataModel>();

            List<YenilemeHizi> list = _YenilemeHiziRepository.GetAll() as List<YenilemeHizi>;

            if (list != null)
            {
                foreach (YenilemeHizi item in list)
                {
                    YenilemeHiziDataModel model = new YenilemeHiziDataModel();
                    model.Id = item.Id;
                    model.YenilemeHiziId = item.YenilemeHiziId;
                    model.YenilemeHiziAdi = item.YenilemeHiziAdi;
                    model.CreatedAt = item.CreatedAt;
                    model.CreatedBy = item.CreatedBy;

                    listYenilemeHizi.Add(model);
                }
            }

            return listYenilemeHizi;
        }

        private YenilemeHizi GetDataModel(YenilemeHiziDataModel model)
        {
            YenilemeHizi item = new YenilemeHizi();

            item.YenilemeHiziId = model.YenilemeHiziId.Value;
            item.YenilemeHiziAdi = model.YenilemeHiziAdi;
            item.CreatedAt = DateTime.Now;
            item.CreatedBy = model.CreatedBy.Value;

            if (model.Id > 0)
            {
                item.Id = model.Id;
            }

            return item;
        }

        public List<YenilemeHiziDataModel> GetYenilemeHiziList(string searchTerm)
        {
            return _YenilemeHiziRepository.GetYenilemeHiziList(searchTerm);
        }

        public YenilemeHizi GetId(int pId)
        {
            return _YenilemeHiziRepository.GetSelect(pId);
        }

        public int Add(YenilemeHiziDataModel item)
        {
            return _YenilemeHiziRepository.Add(GetDataModel(item)).Id;
        }

        public int Update(YenilemeHiziDataModel item)
        {
            return _YenilemeHiziRepository.Update(GetDataModel(item)).Id;
        }

        public YenilemeHizi Delete(int pId)
        {
            return _YenilemeHiziRepository.Delete(pId);
        }
    }
}
