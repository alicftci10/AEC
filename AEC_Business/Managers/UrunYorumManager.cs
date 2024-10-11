using AEC_Business.Interfaces;
using AEC_DataAccess.DBModels;
using AEC_DataAccess.EFInterfaces;
using AEC_Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AEC_Business.Managers
{
    public class UrunYorumManager : BaseManager, IUrunYorumService
    {
        private readonly IEFUrunYorumRepository _UrunYorumRepository;
        public UrunYorumManager(IEFUrunYorumRepository UrunYorumRepository)
        {
            _UrunYorumRepository = UrunYorumRepository;
        }

        public List<UrunYorumDataModel> GetAllUrunYorum()
        {
            List<UrunYorumDataModel> listUrunYorum = new List<UrunYorumDataModel>();

            List<UrunYorum> list = _UrunYorumRepository.GetAll() as List<UrunYorum>;

            if (list != null)
            {
                foreach (UrunYorum item in list)
                {
                    UrunYorumDataModel model = new UrunYorumDataModel();
                    model.Id = item.Id;
                    model.LaptopId = item.LaptopId;
                    model.MonitorId = item.MonitorId;
                    model.MouseId = item.MouseId;
                    model.Yorum = item.Yorum;
                    model.YorumPuan = item.YorumPuan;
                    model.CreatedAt = item.CreatedAt;
                    model.CreatedBy = item.CreatedBy;

                    listUrunYorum.Add(model);
                }
            }

            return listUrunYorum;
        }

        private UrunYorum GetDataModel(UrunYorumDataModel model)
        {
            UrunYorum item = new UrunYorum();

            item.LaptopId = model.LaptopId;
            item.MonitorId = model.MonitorId;
            item.MouseId = model.MouseId;
            item.Yorum = model.Yorum;
            item.YorumPuan = model.YorumPuan;
            item.CreatedAt = DateTime.Now;
            item.CreatedBy = model.CreatedBy.Value;

            if (model.Id > 0)
            {
                item.Id = model.Id;
            }

            return item;
        }

        public List<UrunYorumDataModel> GetUrunYorumLaptopList(int pLaptopId)
        {
            return _UrunYorumRepository.GetUrunYorumList(pLaptopId, null, null);
        }

        public List<UrunYorumDataModel> GetUrunYorumMonitorList(int pMonitorId)
        {
            return _UrunYorumRepository.GetUrunYorumList(null, pMonitorId, null);
        }

        public List<UrunYorumDataModel> GetUrunYorumMouseList(int pMouseId)
        {
            return _UrunYorumRepository.GetUrunYorumList(null, null, pMouseId);
        }

        public List<UrunYorumDataModel> UrunYorumKullaniciList(int pKullaniciId)
        {
            return _UrunYorumRepository.UrunYorumKullaniciList(pKullaniciId);
        }

        public UrunYorum GetId(int pId)
        {
            return _UrunYorumRepository.GetSelect(pId);
        }

        public int Add(UrunYorumDataModel item)
        {
            return _UrunYorumRepository.Add(GetDataModel(item)).Id;
        }

        public int Update(UrunYorumDataModel item)
        {
            return _UrunYorumRepository.Update(GetDataModel(item)).Id;
        }

        public UrunYorum Delete(int pId)
        {
            return _UrunYorumRepository.Delete(pId);
        }
    }
}
