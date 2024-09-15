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
    public class RAMManager : BaseManager, IRAMService
    {
        private readonly IEFRAMRepository _RAMRepository;
        public RAMManager(IEFRAMRepository RAMRepository)
        {
            _RAMRepository = RAMRepository;
        }

        public List<RAMDataModel> GetAllRAM()
        {
            List<RAMDataModel> listRAM = new List<RAMDataModel>();

            List<Ram> list = _RAMRepository.GetAll() as List<Ram>;

            if (list != null)
            {
                foreach (Ram item in list)
                {
                    RAMDataModel model = new RAMDataModel();
                    model.Id = item.Id;
                    model.BellekId = item.BellekId;
                    model.BellekAdi = item.BellekAdi;
                    model.CreatedAt = item.CreatedAt;
                    model.CreatedBy = item.CreatedBy;

                    listRAM.Add(model);
                }
            }

            return listRAM;
        }

        private Ram GetDataModel(RAMDataModel model)
        {
            Ram item = new Ram();

            item.BellekId = model.BellekId.Value;
            item.BellekAdi = model.BellekAdi;
            item.CreatedAt = DateTime.Now;
            item.CreatedBy = model.CreatedBy.Value;

            if (model.Id > 0)
            {
                item.Id = model.Id;
            }

            return item;
        }

        public List<RAMDataModel> GetRAMList(string searchTerm)
        {
            return _RAMRepository.GetRAMList(searchTerm);
        }

        public Ram GetId(int pId)
        {
            return _RAMRepository.GetSelect(pId);
        }

        public int Add(RAMDataModel item)
        {
            return _RAMRepository.Add(GetDataModel(item)).Id;
        }

        public int Update(RAMDataModel item)
        {
            return _RAMRepository.Update(GetDataModel(item)).Id;
        }

        public Ram Delete(int pId)
        {
            return _RAMRepository.Delete(pId);
        }
    }
}
