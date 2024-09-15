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
    public class SSDManager : BaseManager, ISSDService
    {
        private readonly IEFSSDRepository _SSDRepository;
        public SSDManager(IEFSSDRepository SSDRepository)
        {
            _SSDRepository = SSDRepository;
        }

        public List<SSDDataModel> GetAllSSD()
        {
            List<SSDDataModel> listSSD = new List<SSDDataModel>();

            List<Ssd> list = _SSDRepository.GetAll() as List<Ssd>;

            if (list != null)
            {
                foreach (Ssd item in list)
                {
                    SSDDataModel model = new SSDDataModel();
                    model.Id = item.Id;
                    model.DepolamaId = item.DepolamaId;
                    model.DepolamaAdi = item.DepolamaAdi;
                    model.CreatedAt = item.CreatedAt;
                    model.CreatedBy = item.CreatedBy;

                    listSSD.Add(model);
                }
            }

            return listSSD;
        }

        private Ssd GetDataModel(SSDDataModel model)
        {
            Ssd item = new Ssd();

            item.DepolamaId = model.DepolamaId.Value;
            item.DepolamaAdi = model.DepolamaAdi;
            item.CreatedAt = DateTime.Now;
            item.CreatedBy = model.CreatedBy.Value;

            if (model.Id > 0)
            {
                item.Id = model.Id;
            }

            return item;
        }

        public List<SSDDataModel> GetSSDList(string searchTerm)
        {
            return _SSDRepository.GetSSDList(searchTerm);
        }

        public Ssd GetId(int pId)
        {
            return _SSDRepository.GetSelect(pId);
        }

        public int Add(SSDDataModel item)
        {
            return _SSDRepository.Add(GetDataModel(item)).Id;
        }

        public int Update(SSDDataModel item)
        {
            return _SSDRepository.Update(GetDataModel(item)).Id;
        }

        public Ssd Delete(int pId)
        {
            return _SSDRepository.Delete(pId);
        }
    }
}
