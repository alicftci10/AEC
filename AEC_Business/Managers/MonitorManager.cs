using AEC_Business.Interfaces;
using AEC_DataAccess.DBModels;
using AEC_DataAccess.EFInterfaces;
using AEC_Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monitor = AEC_DataAccess.DBModels.Monitor;

namespace AEC_Business.Managers
{
    public class MonitorManager : BaseManager, IMonitorService
    {
        private readonly IEFMonitorRepository _MonitorRepository;
        public MonitorManager(IEFMonitorRepository MonitorRepository)
        {
            _MonitorRepository = MonitorRepository;
        }

        public List<MonitorDataModel> GetAllMonitor()
        {
            List<MonitorDataModel> listMonitor = new List<MonitorDataModel>();

            List<Monitor> list = _MonitorRepository.GetAll() as List<Monitor>;

            if (list != null)
            {
                foreach (Monitor item in list)
                {
                    MonitorDataModel model = new MonitorDataModel();
                    model.Id = item.Id;
                    model.MonitorAdi = item.MonitorAdi;
                    model.Fiyat = item.Fiyat;
                    model.CozunurlukId = item.CozunurlukId;
                    model.YenilemeHiziId = item.YenilemeHiziId;
                    model.EkranYapisi = item.EkranYapisi;
                    model.Hdr = item.Hdr;
                    model.Boyut = item.Boyut;
                    model.Agirlik = item.Agirlik;
                    model.CreatedAt = item.CreatedAt;
                    model.CreatedBy = item.CreatedBy;

                    listMonitor.Add(model);
                }
            }

            return listMonitor;
        }

        private Monitor GetDataModel(MonitorDataModel model)
        {
            Monitor item = new Monitor();

            item.MonitorAdi = model.MonitorAdi;
            item.Fiyat = model.Fiyat.Value;
            item.CozunurlukId = model.CozunurlukId.Value;
            item.YenilemeHiziId = model.YenilemeHiziId.Value;
            item.EkranYapisi = model.EkranYapisi;
            item.Hdr = model.Hdr;
            item.Boyut = model.Boyut;
            item.Agirlik = model.Agirlik;
            item.CreatedAt = DateTime.Now;
            item.CreatedBy = model.CreatedBy.Value;

            if (model.Id > 0)
            {
                item.Id = model.Id;
            }

            return item;
        }

        public List<MonitorDataModel> GetMonitorList(string searchTerm)
        {
            return _MonitorRepository.GetMonitorList(searchTerm);
        }

        public List<MonitorDataModel> GetMonitorList()
        {
            return _MonitorRepository.GetMonitorList();
        }

        public MonitorDataModel GetMonitorId(int pId)
        {
            return _MonitorRepository.GetMonitorId(pId);
        }

        public Monitor GetId(int pId)
        {
            return _MonitorRepository.GetSelect(pId);
        }

        public int Add(MonitorDataModel item)
        {
            return _MonitorRepository.Add(GetDataModel(item)).Id;
        }

        public int Update(MonitorDataModel item)
        {
            return _MonitorRepository.Update(GetDataModel(item)).Id;
        }

        public Monitor Delete(int pId)
        {
            return _MonitorRepository.Delete(pId);
        }
    }
}
