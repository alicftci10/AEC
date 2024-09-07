using AEC_DataAccess.DBContext;
using AEC_DataAccess.DBModels;
using AEC_DataAccess.EFInterfaces;
using AEC_DataAccess.GenericRepository.Repository;
using AEC_Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AEC_DataAccess.EFOperations
{
    public class EFKullaniciKart : GenericRepository<KullaniciKart>, IEFKullaniciKartRepository
    {
        public EFKullaniciKart(AecommerceDbContext AECContext) : base(AECContext) { }

        public List<KullaniciKartDataModel> GetKullaniciKartListesi(int pId)
        {
            using (AecommerceDbContext db = new AecommerceDbContext())
            {
                var kullanicikartList = db.KullaniciKarts.Where(i => i.KullaniciId == pId).Select(i => new KullaniciKartDataModel
                {
                    Id = i.Id,
                    KullaniciId = i.KullaniciId,
                    KartAdi = i.KartAdi,
                    KartNumarasi = i.KartNumarasi,
                    KartAdSoyad = i.KartAdSoyad,
                    KartSktay = i.KartSktay,
                    KartSktyil = i.KartSktyil,
                    KartCvvkodu = i.KartCvvkodu,
                    CreatedAt = i.CreatedAt,
                    CreatedBy = i.CreatedBy

                }).ToList();

                return kullanicikartList;
            }
        }
    }
}
