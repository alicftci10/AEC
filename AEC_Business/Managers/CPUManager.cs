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
    public class CPUManager : BaseManager, ICPUService
    {
        private readonly IEFCPURepository _CPURepository;
        public CPUManager(IEFCPURepository CPURepository)
        {
            _CPURepository = CPURepository;
        }

        public List<CPUDataModel> GetAllCPU()
        {
            List<CPUDataModel> listCPU = new List<CPUDataModel>();

            List<Cpu> list = _CPURepository.GetAll() as List<Cpu>;

            if (list != null)
            {
                foreach (Cpu item in list)
                {
                    CPUDataModel model = new CPUDataModel();
                    model.Id = item.Id;
                    model.IslemciSerisi = item.IslemciSerisi;
                    model.IslemciMimarisi = item.IslemciMimarisi;
                    model.IslemciAdi = item.IslemciAdi;
                    model.CreatedAt = item.CreatedAt;
                    model.CreatedBy = item.CreatedBy;

                    listCPU.Add(model);
                }
            }
            
            return listCPU;
        }

        private Cpu GetDataModel(CPUDataModel model)
        {
            Cpu item = new Cpu();

            item.IslemciSerisi = model.IslemciSerisi;
            item.IslemciMimarisi = model.IslemciMimarisi;
            item.IslemciAdi = model.IslemciAdi;
            item.CreatedAt = DateTime.Now;
            item.CreatedBy = model.CreatedBy.Value;

            if (model.Id > 0)
            {
                item.Id = model.Id;
            }

            return item;
        }

        public List<CPUDataModel> GetCPUList(string searchTerm)
        {
            return _CPURepository.GetCPUList(searchTerm);
        }

        public Cpu GetId(int pId)
        {
            return _CPURepository.GetSelect(pId);
        }

        public int Add(CPUDataModel item)
        {
            return _CPURepository.Add(GetDataModel(item)).Id;
        }

        public int Update(CPUDataModel item)
        {
            return _CPURepository.Update(GetDataModel(item)).Id;
        }

        public Cpu Delete(int pId)
        {
            return _CPURepository.Delete(pId);
        }
    }
}
