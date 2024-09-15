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
    public class EFKullaniciTuru : GenericRepository<KullaniciTuru>, IEFKullaniciTuruRepository
    {
        public EFKullaniciTuru(AecommerceDbContext AECContext) : base(AECContext) { }

        public List<KullaniciTuruDataModel> GetKullaniciTuruList(string searchTerm)
        {
            using (AecommerceDbContext db = new AecommerceDbContext())
            {
                var kullanicituruList = (from k in db.KullaniciTurus

                                         join kul in db.Kullanicis on k.CreatedBy equals kul.Id

                                         select new KullaniciTuruDataModel
                                         {
                                             Id = k.Id,
                                             TurAdi = k.TurAdi,
                                             CreatedAt = k.CreatedAt,
                                             CreatedBy = k.CreatedBy,
                                             CreatedByName = kul.Ad + " " + kul.Soyad

                                         }).ToList();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    searchTerm = searchTerm.ToLower();
                    kullanicituruList = kullanicituruList.Where(i => (!string.IsNullOrEmpty(i.TurAdi) && i.TurAdi.ToLower().Contains(searchTerm)) ||
                                                                     (!string.IsNullOrEmpty(i.CreatedByName) && i.CreatedByName.ToLower().Contains(searchTerm)) ||
                                                                     (i.CreatedAt != null && i.CreatedAt.ToString().Contains(searchTerm))
                                                                     ).ToList();
                }

                return kullanicituruList;
            }
        }

    }
}
