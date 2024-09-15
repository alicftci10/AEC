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
    public class GPUManager : BaseManager, IGPUService
    {
        private readonly IEFGPURepository _GPURepository;
        public GPUManager(IEFGPURepository GPURepository)
        {
            _GPURepository = GPURepository;
        }

        public List<GPUDataModel> GetAllGPU()
        {
            List<GPUDataModel> listGPU = new List<GPUDataModel>();

            List<Gpu> list = _GPURepository.GetAll() as List<Gpu>;

            if (list != null)
            {
                foreach (Gpu item in list)
                {
                    GPUDataModel model = new GPUDataModel();
                    model.Id = item.Id;
                    model.EkranKartiSerisiId = item.EkranKartiSerisiId;
                    model.EkranKartiAdi = item.EkranKartiAdi;
                    model.CreatedAt = item.CreatedAt;
                    model.CreatedBy = item.CreatedBy;

                    listGPU.Add(model);
                }
            }

            return listGPU;
        }

        private Gpu GetDataModel(GPUDataModel model)
        {
            Gpu item = new Gpu();

            item.EkranKartiSerisiId = model.EkranKartiSerisiId.Value;
            item.EkranKartiAdi = model.EkranKartiAdi;
            item.CreatedAt = DateTime.Now;
            item.CreatedBy = model.CreatedBy.Value;

            if (model.Id > 0)
            {
                item.Id = model.Id;
            }

            return item;
        }

        public List<GPUDataModel> GetGPUList(string searchTerm)
        {
            return _GPURepository.GetGPUList(searchTerm);
        }

        public Gpu GetId(int pId)
        {
            return _GPURepository.GetSelect(pId);
        }

        public int Add(GPUDataModel item)
        {
            return _GPURepository.Add(GetDataModel(item)).Id;
        }

        public int Update(GPUDataModel item)
        {
            return _GPURepository.Update(GetDataModel(item)).Id;
        }

        public Gpu Delete(int pId)
        {
            return _GPURepository.Delete(pId);
        }
    }
}
