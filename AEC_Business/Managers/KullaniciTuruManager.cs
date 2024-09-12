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
    public class KullaniciTuruManager : BaseManager, IKullaniciTuruService
    {
        private readonly IEFKullaniciTuruRepository _KullaniciTuruRepository;
        public KullaniciTuruManager(IEFKullaniciTuruRepository KullaniciTuruRepository)
        {
            _KullaniciTuruRepository = KullaniciTuruRepository;
        }

        public List<KullaniciTuruDataModel> GetAllKullaniciTuru()
        {
            List<KullaniciTuruDataModel> listKullaniciTuru = new List<KullaniciTuruDataModel>();

            List<KullaniciTuru> list = _KullaniciTuruRepository.GetAll() as List<KullaniciTuru>;

            if (list != null)
            {
                foreach (KullaniciTuru item in list)
                {
                    KullaniciTuruDataModel model = new KullaniciTuruDataModel();
                    model.Id = item.Id;
                    model.TurAdi = item.TurAdi;
                    model.CreatedAt = item.CreatedAt;
                    model.CreatedBy = item.CreatedBy;

                    listKullaniciTuru.Add(model);
                }
            }

            return listKullaniciTuru;
        }

        private KullaniciTuru GetDataModel(KullaniciTuruDataModel model)
        {
            KullaniciTuru item = new KullaniciTuru();

            item.TurAdi = model.TurAdi;
            item.CreatedAt = DateTime.Now;
            item.CreatedBy = model.CreatedBy.Value;

            if (model.Id > 0)
            {
                item.Id = model.Id;
            }

            return item;
        }

        public List<KullaniciTuruDataModel> GetKullaniciTuruList(string searchTerm)
        {
            return _KullaniciTuruRepository.GetKullaniciTuruList(searchTerm);
        }

        public KullaniciTuru GetId(int pId)
        {
            return _KullaniciTuruRepository.GetSelect(pId);
        }

        public int Add(KullaniciTuruDataModel item)
        {
            return _KullaniciTuruRepository.Add(GetDataModel(item)).Id;
        }

        public int Update(KullaniciTuruDataModel item)
        {
            return _KullaniciTuruRepository.Update(GetDataModel(item)).Id;
        }

        public KullaniciTuru Delete(int pId)
        {
            return _KullaniciTuruRepository.Delete(pId);
        }
    }
}
