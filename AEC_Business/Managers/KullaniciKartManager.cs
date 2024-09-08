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
    public class KullaniciKartManager : BaseManager, IKullaniciKartService
    {
        private readonly IEFKullaniciKartRepository _KullaniciKartRepository;
        public KullaniciKartManager(IEFKullaniciKartRepository KullaniciKartRepository)
        {
            _KullaniciKartRepository = KullaniciKartRepository;
        }

        private KullaniciKart GetDataModel(KullaniciKartDataModel model)
        {
            KullaniciKart item = new KullaniciKart();

            item.KullaniciId = model.KullaniciId;
            item.KartAdi = model.KartAdi;
            item.KartNumarasi = model.KartNumarasi;
            item.KartAdSoyad = model.KartAdSoyad;
            item.KartSktay = model.KartSktay;
            item.KartSktyil = model.KartSktyil;
            item.KartCvvkodu = model.KartCvvkodu;
            item.CreatedAt = DateTime.Now;
            item.CreatedBy = model.CreatedBy.Value;

            if (model.Id > 0)
            {
                item.Id = model.Id;
            }

            return item;
        }

        public List<KullaniciKartDataModel> GetKullaniciKartListesi(int pId)
        {
            return _KullaniciKartRepository.GetKullaniciKartListesi(pId);
        }

        public KullaniciKart GetKullaniciKart(int pId)
        {
            return _KullaniciKartRepository.GetSelect(pId);
        }

        public int Add(KullaniciKartDataModel item)
        {
            return _KullaniciKartRepository.Add(GetDataModel(item)).Id;
        }

        public int Update(KullaniciKartDataModel item)
        {
            return _KullaniciKartRepository.Update(GetDataModel(item)).Id;
        }

        public KullaniciKart Delete(int pId)
        {
            return _KullaniciKartRepository.Delete(pId);
        }
    }
}
