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
            foreach (KullaniciTuru item in list)
            {
                KullaniciTuruDataModel model = new KullaniciTuruDataModel();
                model.Id = item.Id;
                model.KullaniciTuruAd = item.TurAdi;
                model.CreatedAt = item.CreatedAt;
                model.CreatedBy = item.CreatedBy;

                listKullaniciTuru.Add(model);
            }

            return listKullaniciTuru;
        }
    }
}
