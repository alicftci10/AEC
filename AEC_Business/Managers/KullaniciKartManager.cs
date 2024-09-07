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

        public List<KullaniciKartDataModel> GetKullaniciKartListesi(int pId)
        {
            return _KullaniciKartRepository.GetKullaniciKartListesi(pId);
        }
    }
}
